import { Button } from "@fluentui/react-components";
import { Combobox } from "@fluentui/react-components";
import { ComboboxProps } from "@fluentui/react-components";
import { Dialog } from "@fluentui/react-components";
import { DialogActions } from "@fluentui/react-components";
import { DialogBody } from "@fluentui/react-components";
import { DialogContent } from "@fluentui/react-components";
import { DialogProps } from "@fluentui/react-components";
import { DialogTrigger } from "@fluentui/react-components";
import { DialogTitle } from "@fluentui/react-components";
import { DialogSurface } from "@fluentui/react-components";
import { Field } from "@fluentui/react-components";
import { Option } from "@fluentui/react-components";
import { makeStyles } from "@fluentui/react-components";
import { useState } from "react";
import { isNullOrWhiteSpace } from "../../stringExtensions";

const useStyles = makeStyles({
    dialogSurface: {
        width: "fit-content",
    },

    dialogContent: {
        marginTop: "12px",
        marginBottom: "12px",
    },

    dialogActions: {
        display: "flex",
        columnGap: "8px",
    },

    reasonCombobox: {
        width: "360px",
    },
});

type KickDialogProps = Omit<DialogProps, "children" | "modalType"> & {
    onSubmit: (reason: string) => void;
    recentReasons?: readonly string[];
};

const KickDialog = (props: KickDialogProps) => {
    const styles = useStyles();
    const { onSubmit, recentReasons } = props;
    const [reason, setReason] = useState<string | undefined>("");
    const canSubmit = !isNullOrWhiteSpace(reason);
    const validationMessage = !canSubmit ? "Please specify a reason" : undefined;

    const handleSubmitClick = (e: React.MouseEvent) => {
        e.preventDefault();
        onSubmit(reason!);
    };

    const handleComboboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setReason(e.target.value);
    }

    const handleComboboxOptionSelect: ComboboxProps["onOptionSelect"] = (e, data) => {
        setReason(data.optionText);
    }

    return (
        <>
            <Dialog {...props} modalType="alert">
                <DialogSurface className={styles.dialogSurface}>
                    <DialogBody>
                        <DialogTitle>Kick</DialogTitle>

                        <DialogContent className={styles.dialogContent}>
                            <Field
                                required
                                label="Reason"
                                validationMessage={validationMessage}>
                                <Combobox
                                    className={styles.reasonCombobox}
                                    freeform={true}
                                    onChange={handleComboboxChange}
                                    onOptionSelect={handleComboboxOptionSelect}>
                                    {recentReasons && recentReasons.map((value, index) => (
                                        <Option key={index}>{value}</Option>
                                    ))}
                                </Combobox>
                            </Field>
                        </DialogContent>

                        <DialogActions fluid={true}>
                            <div className={styles.dialogActions}>
                                <Button
                                    disabled={!canSubmit}
                                    onClick={handleSubmitClick}>
                                    OK
                                </Button>
                                <DialogTrigger disableButtonEnhancement>
                                    <Button>Close</Button>
                                </DialogTrigger>
                            </div>
                        </DialogActions>
                    </DialogBody>
                </DialogSurface>
            </Dialog>
        </>
    );
};

export type KickDialogSubmitResult = {
    kind: "submit";
    reason: string;
};

export type KickDialogCancelResult = {
    kind: "cancel";
};

export type KickDialogResult = KickDialogSubmitResult | KickDialogCancelResult;

export const useKickDialog = (recentReasons: readonly string[] | undefined, onDialogClosed: (result: KickDialogResult) => void) => {
    const [open, setOpen] = useState(false);

    const showDialog = () => setOpen(true);

    const handleSubmit = (reason: string) => {
        setOpen(false);

        onDialogClosed({
            kind: "submit",
            reason: reason,
        });
    };

    const handleOpenChange: DialogProps["onOpenChange"] = (e, data) => {
        setOpen(data.open);

        if (!data.open) {
            onDialogClosed({
                kind: "cancel",
            });
        }
    };

    const Dialog = () => {
        return (
            <KickDialog
                open={open}
                onOpenChange={handleOpenChange}
                onSubmit={handleSubmit}
                recentReasons={recentReasons}
            />
        );
    };

    return [Dialog, showDialog] as const;
};
