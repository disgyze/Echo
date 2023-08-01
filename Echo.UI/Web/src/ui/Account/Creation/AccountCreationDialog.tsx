import { Button } from "@fluentui/react-components";
import { CompoundButton } from "@fluentui/react-components";
import { Dialog } from "@fluentui/react-components";
import { DialogActions } from "@fluentui/react-components";
import { DialogBody } from "@fluentui/react-components";
import { DialogContent } from "@fluentui/react-components";
import { DialogProps } from "@fluentui/react-components";
import { DialogSurface } from "@fluentui/react-components";
import { DialogTitle } from "@fluentui/react-components";
import { DialogTrigger } from "@fluentui/react-components";
import { memo } from "react";
import { useState } from "react";

const WelcomePage = memo(() => {
    return <div>Welcome</div>;
});

const ChooseAccountPage = memo(() => {
    return (
        <div style={{ display: "flex", flexDirection: "column", rowGap: "8px" }}>
            <CompoundButton
                style={{ justifyContent: "start" }}
                size="large"
                secondaryContent="Register a new account on the server of your choice">
                Register new account
            </CompoundButton>
            <CompoundButton
                style={{ justifyContent: "start" }}
                size="large"
                secondaryContent="Use already existing account">
                Use an existing account
            </CompoundButton>
        </div>
    );
});

export type AccountCreationDialogProps = Omit<DialogProps, "children">;

export const AccountCreationDialog = (props: AccountCreationDialogProps): JSX.Element => {
    const [pageIndex, setPageIndex] = useState(0);
    const canGoNext = pageIndex < 1;
    const canGoBack = pageIndex > 0;

    const handleNextClick = (e: React.MouseEvent) => {
        setPageIndex((prev) => prev + 1);
    };

    const handleBackClick = (e: React.MouseEvent) => {
        setPageIndex((prev) => prev - 1);
    };

    return (
        <>
            <Dialog
                {...props}
                modalType="alert">
                <DialogSurface>
                    <DialogBody style={{ height: "320px" }}>
                        <DialogTitle>Create account</DialogTitle>

                        <DialogContent>
                            {pageIndex === 0 && <WelcomePage />}
                            {pageIndex === 1 && <ChooseAccountPage />}
                        </DialogContent>

                        <DialogActions>
                            <div style={{ display: "flex", columnGap: "8px", marginRight: "12px" }}>
                                <Button
                                    disabled={!canGoBack}
                                    onClick={handleBackClick}>
                                    Back
                                </Button>
                                <Button
                                    disabled={!canGoNext}
                                    onClick={handleNextClick}>
                                    Next
                                </Button>
                            </div>
                            <DialogTrigger disableButtonEnhancement>
                                <Button>Close</Button>
                            </DialogTrigger>
                        </DialogActions>
                    </DialogBody>
                </DialogSurface>
            </Dialog>
        </>
    );
};
