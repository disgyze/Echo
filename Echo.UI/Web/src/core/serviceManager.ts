export interface IServiceProvider {
    getService<T extends object>(key: string): T | null;
}

export interface IServiceManager extends IServiceProvider {
    register<T extends object>(key: string, instanceOrFactory: T | (() => T)): IServiceManager;
}

export class ServiceManager implements IServiceManager {
    private readonly serviceMap = new Map<string, object>();

    getService<T extends object>(key: string): T | null {
        const value = this.serviceMap.get(key);

        if (value) {
            if (typeof value === "object") {
                return value as T;
            } else {
                return (value as () => T)();
            }
        }

        return null;
    }

    register<T extends object>(key: string, instanceOrFactory: T | (() => T)): IServiceManager {
        this.serviceMap.set(key, instanceOrFactory);
        return this;
    }
}
