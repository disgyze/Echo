import { XMPPAddress } from "../../core";
import { Account, Contact, ContactEventDataKind, IAccountService, IContactService, ISubscriptionService, SubscriptionResult } from "../user";

export class NativeAccountService implements IAccountService {
    getAll(): Promise<readonly Account[] | null> {
        throw new Error("Method not implemented.");
    }

    getById(id: string): Promise<Account | null> {
        throw new Error("Method not implemented.");
    }

    getByAddress(address: XMPPAddress): Promise<Account | null> {
        throw new Error("Method not implemented.");
    }

    update(account: Account): Promise<boolean> {
        throw new Error("Method not implemented.");
    }

    remove(account: Account): Promise<boolean> {
        throw new Error("Method not implemented.");
    }
}

export class NativeContactService implements IContactService {
    getAll(accountID: string | undefined = undefined): Promise<readonly Contact[] | null> {
        throw new Error("Method not implemented.");
    }

    get(accountID: string, contactID: string): Promise<Contact | null> {
        throw new Error("Method not implemented.");
    }

    getRange(accountID: string, limit: number, offset: number): Promise<readonly Contact[] | null> {
        throw new Error("Method not implemented.");
    }

    update(accountID: string, contact: Contact): Promise<void> {
        throw new Error("Method not implemented.");
    }

    remove(accountID: string, contact: Contact): Promise<void> {
        throw new Error("Method not implemented.");
    }

    removeByID(accountID: string, contactID: string): Promise<void> {
        throw new Error("Method not implemented.");
    }

    subscribe<T extends ContactEventDataKind>(handler: (e: T) => void): () => void {
        throw new Error("Method not implemented.");
    }
}

export class NativeSubscriptionService implements ISubscriptionService {
    subscribe(address: XMPPAddress): Promise<SubscriptionResult> {
        throw new Error("Method not implemented.");
    }

    unsubscribe(address: XMPPAddress): Promise<SubscriptionResult> {
        throw new Error("Method not implemented.");
    }

    approve(address: XMPPAddress): Promise<SubscriptionResult> {
        throw new Error("Method not implemented.");
    }

    deny(address: XMPPAddress): Promise<SubscriptionResult> {
        throw new Error("Method not implemented.");
    }
}