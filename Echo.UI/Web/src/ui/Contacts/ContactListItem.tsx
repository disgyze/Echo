import { Button } from "@fluentui/react-components";
import { Persona } from "@fluentui/react-components";
import { PresenceBadgeStatus } from "@fluentui/react-components";
import { makeStyles } from "@fluentui/react-components";
import { Contact } from "../../core/user";
import { PresenceStatus } from "../../core/user";

const convertPresence = (presenceStatus: PresenceStatus): PresenceBadgeStatus => {
    switch (presenceStatus) {
        case "online":
        case "freeForChat":
            return "available";

        case "offline":
            return "offline";

        case "away":
            return "away";

        case "doNotDisturb":
            return "do-not-disturb";

        case "extendedAway":
            return "out-of-office";
    }
};

const useStyles = makeStyles({
    button: {
        display: "flex",
        flexDirection: "column",
        alignItems: "start",
        minHeight: "44px",
        marginLeft: "2px",
        marginRight: "2px",
        paddingLeft: "24px",
        paddingRight: "24px",
    },

    persona: {
        pointerEvents: "none",
    },
});

export type ContactListItemProps = {
    contact: Contact;
    onClick?: (contact: Contact) => void;
};

export const ContactListItem = ({ contact, onClick }: ContactListItemProps) => {
    const styles = useStyles();

    const handleClick = (e: React.MouseEvent) => {
        if (onClick) {
            onClick(contact);
        }
    };

    return (
        <Button
            data-id="contact-item"
            data-contact-id={contact.id}
            shape="square"
            appearance="subtle"
            className={styles.button}
            onClick={handleClick}>
            <Persona
                avatar={{ color: "colorful" }}
                textAlignment="center"
                name={contact.nick}
                presence={{ status: convertPresence(contact.presence.status) }}
                className={styles.persona}
            />
        </Button>
    );
};
