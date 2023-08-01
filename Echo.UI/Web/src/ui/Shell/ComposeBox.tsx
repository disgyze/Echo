import { Textarea } from "@fluentui/react-components";
import { Toolbar } from "@fluentui/react-components";
import { ToolbarButton } from "@fluentui/react-components";
import { ToolbarGroup } from "@fluentui/react-components";
import { Tooltip } from "@fluentui/react-components";
import { makeStyles } from "@fluentui/react-components";
import { shorthands } from "@fluentui/react-components";
import { bundleIcon } from "@fluentui/react-icons";
import { Attach20Filled } from "@fluentui/react-icons";
import { Attach20Regular } from "@fluentui/react-icons";
import { DrawText20Filled } from "@fluentui/react-icons";
import { DrawText20Regular } from "@fluentui/react-icons";
import { Emoji20Filled } from "@fluentui/react-icons";
import { Emoji20Regular } from "@fluentui/react-icons";
import { Send20Filled } from "@fluentui/react-icons";
import { Send20Regular } from "@fluentui/react-icons";
import { Sticker20Filled } from "@fluentui/react-icons";
import { Sticker20Regular } from "@fluentui/react-icons";

const DrawTextIcon = bundleIcon(DrawText20Filled, DrawText20Regular);
const StickerIcon = bundleIcon(Sticker20Filled, Sticker20Regular);
const AttachIcon = bundleIcon(Attach20Filled, Attach20Regular);
const EmojiIcon = bundleIcon(Emoji20Filled, Emoji20Regular);
const SendIcon = bundleIcon(Send20Filled, Send20Regular);

const useStyles = makeStyles({
    root: {
        display: "flex",
        flexDirection: "column",
    },

    textArea: {
        width: "100%",
    },

    innerTextArea: {
        height: "min-content",
        minHeight: "auto",
    },

    toolbar: {
        ...shorthands.padding("0"),
        marginTop: "2px",
        columnGap: "0px",
        justifyContent: "space-between",
    },

    toolbarGroup: {
        columnGap: "0px",
    },
});

export type ComposeBoxProps = {};

export const ComposeBox = () => {
    const styles = useStyles();

    return (
        <div className={styles.root}>
            <Textarea
                className={styles.textArea}
                spellCheck={true}
                rows={1}
                placeholder="Type a message"
                textarea={{ className: styles.innerTextArea }}
            />
            <Toolbar
                size="small"
                className={styles.toolbar}>
                <ToolbarGroup
                    role="presentation"
                    className={styles.toolbarGroup}>
                    <Tooltip
                        content="Formatting"
                        relationship="label">
                        <ToolbarButton
                            aria-label="Formatting"
                            icon={<DrawTextIcon />}
                        />
                    </Tooltip>
                    <Tooltip
                        content="Attach files"
                        relationship="label">
                        <ToolbarButton
                            aria-label="Attach files"
                            icon={<AttachIcon />}
                        />
                    </Tooltip>
                    <Tooltip
                        content="Emoji"
                        relationship="label">
                        <ToolbarButton
                            aria-label="Emoji"
                            icon={<EmojiIcon />}
                        />
                    </Tooltip>
                    <Tooltip
                        content="Stickers"
                        relationship="label">
                        <ToolbarButton
                            aria-label="Stickers"
                            icon={<StickerIcon />}
                        />
                    </Tooltip>
                </ToolbarGroup>

                <ToolbarGroup
                    role="presentation"
                    className={styles.toolbarGroup}>
                    <Tooltip
                        content="Send"
                        relationship="label">
                        <ToolbarButton
                            aria-label="Send"
                            icon={<SendIcon />}
                        />
                    </Tooltip>
                </ToolbarGroup>
            </Toolbar>
        </div>
    );
};
