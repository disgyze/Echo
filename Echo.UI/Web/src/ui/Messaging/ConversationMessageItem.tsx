import { Avatar } from "@fluentui/react-components";
import { Link } from "@fluentui/react-components";
import { Text } from "@fluentui/react-components";
import { Tooltip } from "@fluentui/react-components";
import { makeStyles } from "@fluentui/react-components";
import { mergeClasses } from "@fluentui/react-components";
import { shorthands } from "@fluentui/react-components";
import { tokens } from "@fluentui/react-components";
import { typographyStyles } from "@fluentui/react-components";
import { Eye16Filled } from "@fluentui/react-icons";

const useStyles = makeStyles({
    root: {
        display: "flex",
        columnGap: "12px",
        alignSelf: "start",
    },

    avatar: {
        marginTop: "12px",
    },

    headerContainer: {
        display: "flex",
        columnGap: "12px",
        marginBottom: "2px",
    },

    receiptContainer: {
        display: "flex",
        flexDirection: "row",
        columnGap: "4px",
        alignItems: "center",
    },

    nick: {
        ...typographyStyles.caption1,
        color: tokens.colorNeutralForeground4,
    },

    timestamp: {
        ...typographyStyles.caption1,
        color: tokens.colorNeutralForeground4,
    },

    receiptIcon: {
        ...shorthands.margin("auto"),
        display: "block",
        color: tokens.colorNeutralForeground4,
    },

    text: {
        userSelect: "text",
    },
});

const useBubbleStyles = makeStyles({
    base: {
        userSelect: "text",
        ...shorthands.borderRadius(tokens.borderRadiusMedium),
        ...shorthands.padding("12px"),
    },

    incoming: {
        backgroundColor: tokens.colorBrandBackground2,
    },

    outgoing: {
        backgroundColor: tokens.colorNeutralBackground3,
    },
});

export enum ConversationMessageReceipt {
    Seen,
    Sent,
    Sending,
}

export enum ConversationMessageDirection {
    Incoming,
    Outgoing,
}

export type ConversationMessageItemTextContent = Readonly<{
    kind: "text",
    text: string;
}>

export type ConversationMessageItemImageGalleryContent = Readonly<{
    kind: "imageGallery",
    images: URL[];
}>

export type ConversationMessageItemRetractedContent = Readonly<{
    kind: "retracted",
    address: string;
    reason?: string;
}>

export type ConversationMessageItemReply = Readonly<{
    kind: "reply",
    toMessageID: string;
    toAddress: string;
}>

export type ConversationItemContent = ConversationMessageItemTextContent | ConversationMessageItemImageGalleryContent;

export type ConversationMessageProps = {
    id: string;
    nick: string;
    address: string;
    timestamp: number;
    direction: ConversationMessageDirection;
    content: ConversationItemContent;
    reply?: ConversationMessageItemReply;
    reactions?: string[];
};

export const ConversationMessageItem = ({ id, nick, address, timestamp, direction, content, reply, reactions }: ConversationMessageProps) => {
    const styles = useStyles();
    const bubbleStyles = useBubbleStyles();

    return (
        <div
            className={styles.root}
            data-message-id={id}
            data-message-participant-address={address}>
            <Avatar
                name={nick}
                color="colorful"
                className={styles.avatar}
            />
            <div className={mergeClasses(bubbleStyles.base, direction == ConversationMessageDirection.Incoming ? bubbleStyles.incoming : bubbleStyles.outgoing)}>
                <div className={styles.headerContainer}>
                    <Link appearance="subtle">
                        <Text className={styles.nick}>{nick}</Text>
                    </Link>
                    <div className={styles.receiptContainer}>
                        <Text className={styles.timestamp}>{timestamp}</Text>
                        <Tooltip
                            relationship="description"
                            content="Seen"
                            hideDelay={0}
                            showDelay={0}>
                            <span>
                                <Eye16Filled className={styles.receiptIcon} />
                            </span>
                        </Tooltip>
                    </div>
                </div>
                {/* <Text className={styles.text}>{text}</Text> */}
            </div>
        </div>
    );
};
