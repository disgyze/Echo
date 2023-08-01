import { XMPPAddress } from "../core";

export type PresenceStatus =
    | "online"
    | "offline"
    | "freeForChat"
    | "doNotDisturb"
    | "away"
    | "extendedAway"

export class Presence {
    private constructor(readonly status: PresenceStatus, readonly text: string | undefined = undefined) {
    }

    static online(text: string | undefined = undefined) {
        return new Presence("online", text);
    }

    static offline(text: string | undefined = undefined) {
        return new Presence("offline", text);
    }

    static freeForChat(text: string | undefined = undefined) {
        return new Presence("freeForChat", text);
    }

    static doNotDisturb(text: string | undefined = undefined) {
        return new Presence("doNotDisturb", text);
    }

    static away(text: string | undefined = undefined) {
        return new Presence("away", text);
    }

    static extendedAway(text: string | undefined = undefined) {
        return new Presence("extendedAway", text);
    }
}

export type Subscription =
    | "none"
    | "from"
    | "to"
    | "both"

export type SubscriptionResult =
    | Readonly<{ kind: "success" }>
    | Readonly<{ kind: "failure", reason: string }>

export interface ISubscriptionService {
    subscribe(address: XMPPAddress): Promise<SubscriptionResult>;
    unsubscribe(address: XMPPAddress): Promise<SubscriptionResult>;
    approve(address: XMPPAddress): Promise<SubscriptionResult>;
    deny(address: XMPPAddress): Promise<SubscriptionResult>;
}

export class Contact {
    constructor(
        readonly id: string,
        readonly address: XMPPAddress,
        readonly nick: string | undefined = undefined,
        readonly presence: Presence = Presence.offline(),
        readonly subscription: Subscription = "none") {
    }
}

export abstract class ContactEventData {
    constructor(readonly contact: Contact) {
    }
}

export class ContactAddedEventData extends ContactEventData {
}

export class ContactRemovedEventData extends ContactEventData {
}

export class ContactUpdatedEventData extends ContactEventData {
}

export class ContactPresenceChangedEventData extends ContactEventData {
    constructor(readonly contact: Contact, readonly oldPresence: Presence) {
        super(contact);
    }
}

export type ContactEventDataKind =
    | ContactAddedEventData
    | ContactRemovedEventData
    | ContactUpdatedEventData
    | ContactPresenceChangedEventData

export interface IContactService {
    getAll(accountID?: string | undefined): Promise<ReadonlyArray<Contact> | null>;
    get(accountID: string, contactID: string): Promise<Contact | null>;
    getRange(accountID: string, limit: number, offset: number): Promise<ReadonlyArray<Contact> | null>;
    update(accountID: string, contact: Contact): Promise<void>;
    remove(accountID: string, contact: Contact): Promise<void>;
    removeByID(accountID: string, contactID: string): Promise<void>;
    subscribe<T extends ContactEventDataKind>(handler: (e: T) => void): () => void;
}

export class Account {
    constructor(
        readonly id: string,
        readonly displayName: string,
        readonly nick: string,
        readonly address: XMPPAddress,
        readonly password: string,
        readonly presence: Presence = Presence.offline()) {
    }
}

export interface IAccountService {
    getAll(): Promise<ReadonlyArray<Account> | null>;
    getById(id: string): Promise<Account | null>;
    getByAddress(address: XMPPAddress): Promise<Account | null>;
    update(account: Account): Promise<boolean>;
    remove(account: Account): Promise<boolean>;
}

export enum UserServiceKey {
    ContactService = "ContactService",
    AccountService = "AccountService",
    SubscriptionService = "SubscriptionService"
}