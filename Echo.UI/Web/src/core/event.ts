export interface ICoreEventSubscriber<T> {
    subscribe(handler: (e: T) => void): () => void;
}

export interface ICoreEventPublisher<T> {
    publish(e: T): void;
}

export interface ICoreEvent<T> extends ICoreEventPublisher<T>, ICoreEventSubscriber<T> {
}

export class CoreEvent<T> implements ICoreEvent<T> {
    private readonly handlerList: ((e: T) => void)[] = [];

    subscribe(handler: (e: T) => void): () => void {
        this.handlerList.push(handler);
        return () => {
            const index = this.handlerList.indexOf(handler);

            if (index >= 0) {
                this.handlerList.splice(index, 1);
            }
        };
    }

    publish(e: T): void {
        this.handlerList.forEach(handler => handler(e));
    }
}

export interface ICoreEventService {
    getEvent<T>(name: string): ICoreEvent<T> | null;
}

export class CoreEventService implements ICoreEventService {
    constructor(readonly eventMap: ReadonlyMap<string, object>) {
    }

    getEvent<T>(name: string): ICoreEvent<T> | null {
        return this.eventMap.get(name) as ICoreEvent<T>;
    }
}