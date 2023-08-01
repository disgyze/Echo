import {
    Menu,
    MenuDivider,
    MenuItem,
    MenuList,
    MenuPopover,
    MenuProps,
    PositioningImperativeRef,
    makeStyles,
    shorthands,
    useArrowNavigationGroup,
} from "@fluentui/react-components";
import {
    Call20Filled,
    Call20Regular,
    Delete20Filled,
    Delete20Regular,
    Edit20Filled,
    Edit20Regular,
    Pin20Filled,
    Pin20Regular,
    bundleIcon,
} from "@fluentui/react-icons";
import { OffsetObject } from "@fluentui/react-positioning";
import { useEffect, useRef, useState } from "react";
import { Contact, Presence, PresenceStatus } from "../../core/user";
import { ContactListItem } from "./ContactListItem";
import { AccountCreationDialog } from "../Account/Creation/AccountCreationDialog";
import { KickDialogResult, useKickDialog } from "../Messaging/KickDialog";

const PinIcon = bundleIcon(Pin20Filled, Pin20Regular);
const CallIcon = bundleIcon(Call20Filled, Call20Regular);
const EditIcon = bundleIcon(Edit20Filled, Edit20Regular);
const DeleteIcon = bundleIcon(Delete20Filled, Delete20Regular);

const useStyles = makeStyles({
    input: {
        ...shorthands.border(0),
        marginLeft: "24px",
        marginRight: "24px",
    },

    contactList: {
        paddingTop: "2px",
        paddingBottom: "24px",
        display: "flex",
        flexDirection: "column",
        alignItems: "stretch",
        // @ts-ignore
        overflowY: "overlay",
    },
});

// function sortByName(a: Contact, b: Contact): number {
//     if (!a.nick || !b.nick) {
//         return 0;
//     }

//     if (a.nick < b.nick) {
//         return -1;
//     }

//     if (a.nick > b.nick) {
//         return 1;
//     }

//     return 0;
// }

// online and freeForChat statuses have the same priority, 'cause they do mean basically the same thing in XMPP
const presencePriorityMap: ReadonlyMap<PresenceStatus, number> = new Map([
    ["online", 1],
    ["freeForChat", 1],
    ["doNotDisturb", 2],
    ["away", 3],
    ["extendedAway", 4],
    ["offline", 5],
]);

const nickComparer = new Intl.Collator(undefined, { usage: "sort", sensitivity: "accent" });

const sortByNick = (left: Contact, right: Contact) => {
    if (!left.nick || !right.nick) {
        return 0;
    }
    return nickComparer.compare(left.nick, right.nick);
};

const sortByPresence = (left: Contact, right: Contact) => {
    const leftPriority = presencePriorityMap.get(left.presence.status);
    const rightPriorty = presencePriorityMap.get(right.presence.status);

    if (!leftPriority || !rightPriorty) {
        return 0;
    }

    return leftPriority - rightPriorty;
};

export const ContactList = () => {
    const styles = useStyles();
    const arrowNavigation = useArrowNavigationGroup({ axis: "vertical" });

    const [contacts, setContacts] = useState<Contact[]>([]);
    const [isAccountCreationDialogOpen, setIsAccountCreationDialogOpen] = useState(false);
    const [isKickDialogOpen, setIsKickDialogOpen] = useState(false);

    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [canCall, setCanCall] = useState(false);
    const [menuOffset, setMenuOffset] = useState<OffsetObject>({ crossAxis: 0, mainAxis: 0 });
    const menuPosRef = useRef<PositioningImperativeRef>(null);

    const recentReasons = ["Persona non grata", "See ya"];

    const handleKickDialogClosed = (result: KickDialogResult) => {
        switch (result.kind) {
            case "submit": {
                console.log("KickDialog: Submitted");
                console.log(result.reason);
                break;
            }

            case "cancel": {
                console.log("KickDialog: Cancelled");
                break;
            }
        }
    }

    const [KickDialog, showKickDialog] = useKickDialog(recentReasons, handleKickDialogClosed);

    useEffect(() => {
        const contacts = [
            new Contact(
                "1",
                { user: "tom", domain: "klient.com" },
                "John Connor",
                Presence.online()
            ),
            new Contact(
                "2",
                { user: "khaled", domain: "mirc.com" },
                "Terminator",
                Presence.away()
            ),
            new Contact(
                "3",
                { user: "moxy", domain: "mirc.com" },
                "Sarah Connor",
                Presence.online()
            ),
            new Contact(
                "4",
                { user: "moxy", domain: "mirc.com" },
                "T1000",
                Presence.offline()
            ),
        ];
        setContacts(contacts.sort(sortByPresence));
    }, []);

    const handleContactItemClick = (contact: Contact) => {
        // setIsAccountCreationDialogOpen(true);
        showKickDialog();
    };

    const handleContextMenu = (e: React.MouseEvent<HTMLDivElement>) => {
        const element = document.elementFromPoint(e.clientX, e.clientY);

        if (!element) {
            return;
        }

        const elementID = element.getAttribute("data-id");

        if (elementID === "contact-item") {
            const contactID = element.getAttribute("data-contact-id");
            const contact = contacts.find((contact) => contact.id === contactID);

            if (contact) {
                setCanCall(contact.presence.status !== "offline");
            }

            const rect = element.getBoundingClientRect();
            menuPosRef.current?.setTarget(element);
            setMenuOffset({ crossAxis: e.clientX - rect.x, mainAxis: e.clientY - rect.y });
            setIsMenuOpen((open) => !open);

            e.preventDefault();
        }
    };

    const onOpenChange: MenuProps["onOpenChange"] = (_, data) => {
        setIsMenuOpen(data.open);
    };

    return (
        <>
            {/* <Input className={styles.input} appearance="filled-darker" placeholder="Search for contacts" contentBefore={<Search20Regular />} /> */}

            <div
                {...arrowNavigation}
                className={styles.contactList}
                onContextMenu={handleContextMenu}>
                {contacts.map((contact) => (
                    <ContactListItem
                        key={contact.id}
                        contact={contact}
                        onClick={handleContactItemClick}
                    />
                ))}
            </div>

            <Menu
                positioning={{ offset: menuOffset, coverTarget: true, positioningRef: menuPosRef }}
                open={isMenuOpen}
                openOnContext={true}
                onOpenChange={onOpenChange}>
                <MenuPopover>
                    <MenuList>
                        <MenuItem
                            icon={<CallIcon />}
                            disabled={!canCall}>
                            Call
                        </MenuItem>
                        <MenuDivider />
                        <MenuItem icon={<PinIcon />}>Pin</MenuItem>
                        <MenuItem icon={<EditIcon />}>Edit...</MenuItem>
                        <MenuItem icon={<DeleteIcon />}>Delete</MenuItem>
                    </MenuList>
                </MenuPopover>
            </Menu>

            {isAccountCreationDialogOpen &&
                <AccountCreationDialog
                    defaultOpen={true}
                    onOpenChange={(_, data) => setIsAccountCreationDialogOpen(data.open)} />}

            <KickDialog />
        </>
    );
};
