export class XMPPAddress {
    constructor(
        readonly domain: string,
        readonly user?: string | undefined,
        readonly resource?: string | undefined) {
    }
}