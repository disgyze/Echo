import { XMPPAddress } from "../core";

export type ConversationMessageDirection =
    | "in"
    | "out"

export type ConversationMessage = Readonly<{
    accountID: string;
    connectionID: string
    id: string;
    nick: string;
    address: XMPPAddress;
    text: string;
    timestamp: number;
    direction: ConversationMessageDirection;
}>

export type Conversation = Readonly<{
    id: string;
    accountID: string;
    connectionID: string;
    address: XMPPAddress;
}>

export type Chat = Conversation;

export type ChannelMember = Readonly<{
    channelID: string;
    nick: string;
    address: XMPPAddress;
}>

export type ChannelKind =
    | "mix"
    | "muc"

export type Channel = Conversation & Readonly<{
    kind: ChannelKind;
}>

export type ChannelKickResult =
    | Readonly<{ kind: "success" }>
    | Readonly<{ kind: "failure", reason: string }>

export type ChannelJoinResult =
    | Readonly<{ kind: "success" }>
    | Readonly<{ kind: "failure", reason: string }>

export interface IChannelService {
    getAll(accountID?: string | undefined): Promise<ReadonlyArray<Channel> | null>;
    getRange(limit: number, offset: number, accountID?: string | undefined): Promise<ReadonlyArray<Channel> | null>;
    getByID(accountID: string, channelID: string): Promise<Channel | null>;
    getByAddress(accountID: string, channelAddress: XMPPAddress): Promise<Channel | null>;
    join(accountID: string, channelAddress: XMPPAddress): Promise<ChannelKickResult>;
    leave(accountID: string, channelAddress: XMPPAddress): Promise<void>;
}

export interface IMucChannelService {
    ban(channelID: string, memberAddress: XMPPAddress, reason: string | undefined): Promise<ChannelKickResult>;
    kick(channelID: string, memberAddress: XMPPAddress, reason: string | undefined): Promise<ChannelKickResult>;
}

export interface IMixChannelService {
    kick(channelID: string, memberAddress: XMPPAddress, reason: string | undefined): Promise<ChannelKickResult>;
}