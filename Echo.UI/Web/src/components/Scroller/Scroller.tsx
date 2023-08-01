import { makeStyles, mergeClasses } from "@fluentui/react-components";
import { HTMLAttributes, PropsWithChildren } from "react";

const useStyles = makeStyles({
    root: {
        // @ts-ignore
        overflowY: "overlay"
    }
});

export type ScrollerProps = PropsWithChildren & HTMLAttributes<HTMLDivElement>;

export const Scroller = (props: ScrollerProps) => {
    const styles = useStyles();
    const { className, children } = props;

    return (
        <div {...props} className={mergeClasses(styles.root, className)}>
            {children}
        </div>
    )
}