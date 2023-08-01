export type XMPPConnectionState =
    | "connected"
    | "connecting"
    | "disconnected"

export class XMPPConnection {
    constructor(
        readonly id: string,
        readonly accountID: string,
        readonly host: string,
        readonly port: number,
        readonly isEncrypted: boolean,
        readonly state: XMPPConnectionState) {
    }
}

export abstract class XMPPConnectionEventData {
    constructor(readonly connectionID: string) {
    }
}

export class XMPPConnectionStateChanged extends XMPPConnectionEventData {
}

export class XMPPConnectionAddedChanged extends XMPPConnectionEventData {
}

export class XMPPConnectionRemovedChanged extends XMPPConnectionEventData {
}

export type XMPPConnectionEventDataKind =
    | XMPPConnectionAddedChanged
    | XMPPConnectionRemovedChanged
    | XMPPConnectionStateChanged

export interface IXMPPConnectionService {
    getAll(): Promise<Readonly<XMPPConnection[]> | null>;
    getByID(connectionID: string): Promise<XMPPConnection | null>;
    getByAccountID(accountID: string): Promise<XMPPConnection | null>;
    subscribe<T extends XMPPConnectionEventDataKind>(handler: (e: T) => void): () => void;
}