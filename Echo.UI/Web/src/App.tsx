import { Avatar, Caption1, Input, Link, SelectTabData, SelectTabEvent, Tab, TabList, TabValue, Text, Toolbar, ToolbarButton, Tooltip, makeStyles, shorthands, tokens, typographyStyles } from "@fluentui/react-components";
import { Eye16Filled, Filter20Filled, Filter20Regular, Search20Regular, bundleIcon } from "@fluentui/react-icons";
import { memo, useEffect, useState } from "react";
import { ContactList } from "./ui/Contacts";
import { useFetchContacts } from "./ui/Contacts/useFetchContacts";
import { ComposeBox } from "./ui/Shell/ComposeBox";

const FilterIcon = bundleIcon(Filter20Filled, Filter20Regular);

const useStyles = makeStyles({
    root: {
        display: "grid",
        gridTemplateColumns: "auto 320px 1fr",
        gridTemplateRows: "1fr auto min-content",
        height: "100vh",
        backgroundColor: tokens.colorNeutralBackground3,
    },

    sidebar: {
        gridColumnStart: "2",
        gridRowStart: "span 3",
        display: "flex",
        flexDirection: "column",
        width: "320px",
        backgroundColor: tokens.colorNeutralBackground1,
    },

    tabList: {
        marginTop: "2px",
        marginLeft: "12px",
        marginRight: "12px",
    },

    contentTitle: {
        gridColumnStart: "3",
        ...shorthands.padding("12px", "24px"),
    },

    contentScroller: {
        gridRowStart: "2",
        gridColumnStart: "3",
        display: "flex",
        flexDirection: "column",
        // @ts-ignore
        overflowY: "overlay",
        paddingLeft: "15%",
        paddingRight: "15%",
    },

    chatList: {
        height: "100%",
        display: "flex",
        flexDirection: "column",
        rowGap: "12px",
        paddingTop: "24px",
        paddingLeft: "24px",
        paddingRight: "12px",
        marginRight: "44px",
    },

    composeBox: {
        gridColumnStart: "3",
        gridRowStart: "3",
        marginLeft: "15%",
        marginRight: "15%",
        marginBottom: "12px",
        paddingTop: "12px",
        paddingLeft: "68px",
        paddingRight: "72px",
    },

    tabListContainer: {
        display: "flex",
        flexDirection: "row",
        columnGap: "12px",
        justifyContent: "space-between",
    }
});

enum TabKey {
    ChatList,
    ContactList,
}

const ContactListTab = memo(() => {
    return <ContactList />;
});

const ChatListTab = memo(() => {
    return (
        <Input
            placeholder="Search for chats"
            appearance="filled-darker"
            contentBefore={<Search20Regular />}
            style={{
                border: "0",
                marginLeft: "24px",
                marginRight: "24px",
                marginBottom: "12px",
            }}
        />
    );
});

const App = () => {
    const styles = useStyles();
    const [selectedTab, setSelectedTab] = useState<TabValue>(TabKey.ChatList);

    const [fetchContacts, isLoading] = useFetchContacts();
    // useEffect(() => {
    //     fetchContacts()
    //         .then((result) => console.log(result))
    //         .catch((error) => console.log(error));
    // }, []);

    useEffect(() => {
        // const get = async () => {
        //     const url = "https://jsonplaceholder.typicode.com/todos/1";
        //     // const response = await fetch(url);
        //     // const json = await response.json();
        //     // console.log(json);
        //     fetch(url)
        //         .then((response) => response.json())
        //         .then((json) => console.log(json));
        // };
        // get();
    }, []);

    const handleTabSelect = (_: SelectTabEvent, data: SelectTabData) => {
        setSelectedTab(data.value);
    };

    return (
        <div className={styles.root}>

            <div className={styles.sidebar}>
                <div className={styles.tabListContainer}>
                    <TabList
                        className={styles.tabList}
                        selectedValue={selectedTab}
                        style={{ marginBottom: "12px" }}
                        onTabSelect={handleTabSelect}>
                        <Tab value={TabKey.ChatList}>Chats</Tab>
                        <Tab value={TabKey.ContactList}>Contacts</Tab>
                    </TabList>

                    <Toolbar>
                        <Tooltip
                            relationship="label"
                            content="Filter contacts">
                            <ToolbarButton icon={<FilterIcon />} />
                        </Tooltip>
                    </Toolbar>
                </div>

                {selectedTab === TabKey.ChatList && <ChatListTab />}
                {selectedTab === TabKey.ContactList && <ContactListTab />}
            </div>

            <div className={styles.contentTitle}>
                <div style={{ display: "flex", flexDirection: "column" }}>
                    <Text
                        style={{
                            ...typographyStyles.body1Strong,
                            userSelect: "text",
                        }}>
                        judgment day
                    </Text>
                    <Caption1>3 participants</Caption1>
                </div>
            </div>

            <div className={styles.contentScroller}>
                <div className={styles.chatList}>
                    <div
                        style={{
                            display: "flex",
                            columnGap: "12px",
                            alignSelf: "start",
                        }}>
                        <Avatar
                            name="John Connor"
                            color="colorful"
                            style={{ marginTop: "12px" }}
                        />
                        <div
                            style={{
                                borderRadius: "4px",
                                padding: "12px",
                                backgroundColor: tokens.colorBrandBackground2,
                            }}>
                            <div
                                style={{
                                    display: "flex",
                                    columnGap: "12px",
                                    marginBottom: "2px",
                                }}>
                                <Link appearance="subtle" style={{
                                    ...typographyStyles.caption1,
                                    color: tokens.colorNeutralForeground4,
                                }}>
                                    John Connor
                                </Link>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        columnGap: "4px",
                                        alignItems: "center",
                                    }}>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        15:56
                                    </Text>
                                    <Tooltip
                                        relationship="description"
                                        content="Seen"
                                        hideDelay={0}
                                        showDelay={0}>
                                        <span>
                                            <Eye16Filled
                                                style={{
                                                    display: "block",
                                                    margin: "auto",
                                                    color: tokens.colorNeutralForeground4,
                                                }}
                                            />
                                        </span>
                                    </Tooltip>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>No, no, no, no. You gotta listen to the way people talk. You don't say 'affirmative', or some shit like that. You say no problemo'. If someone comes up to you with an attitude you say eat m. And if you want to shine them on, it's 'Hasta la vista, baby'.</Text>
                        </div>
                    </div>

                    <div
                        style={{
                            display: "flex",
                            columnGap: "12px",
                            alignSelf: "start",
                        }}>
                        <Avatar
                            name="Terminator"
                            color="colorful"
                            style={{ marginTop: "12px" }}
                        />
                        <div
                            style={{
                                borderRadius: "4px",
                                padding: "12px",
                                backgroundColor: tokens.colorNeutralForegroundInverted,
                            }}>
                            <div
                                style={{
                                    display: "flex",
                                    columnGap: "12px",
                                    marginBottom: "2px",
                                }}>
                                <Link>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        Terminator
                                    </Text>
                                </Link>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        columnGap: "4px",
                                        alignItems: "center",
                                    }}>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        15:47
                                    </Text>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>Hasta la vista, baby</Text>
                        </div>
                    </div>

                    <div
                        style={{
                            display: "flex",
                            columnGap: "12px",
                            alignSelf: "start",
                        }}>
                        <Avatar
                            name="John Connor"
                            color="colorful"
                            style={{ marginTop: "12px" }}
                        />
                        <div
                            style={{
                                borderRadius: "4px",
                                padding: "12px",
                                backgroundColor: tokens.colorBrandBackground2,
                            }}>
                            <div
                                style={{
                                    display: "flex",
                                    columnGap: "12px",
                                    marginBottom: "2px",
                                }}>
                                <Link appearance="subtle">
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        John Connor
                                    </Text>
                                </Link>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        columnGap: "4px",
                                        alignItems: "center",
                                    }}>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        15:56
                                    </Text>
                                    <Tooltip
                                        relationship="description"
                                        content="Seen"
                                        hideDelay={0}
                                        showDelay={0}>
                                        <span>
                                            <Eye16Filled
                                                style={{
                                                    display: "block",
                                                    margin: "auto",
                                                    color: tokens.colorNeutralForeground4,
                                                }}
                                            />
                                        </span>
                                    </Tooltip>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>Or 'Later, dickwad'. And if someone gets upset you say, 'chill out'! Or do combinations.</Text>
                        </div>
                    </div>

                    <div
                        style={{
                            display: "flex",
                            columnGap: "12px",
                            alignSelf: "start",
                        }}>
                        <Avatar
                            name="Terminator"
                            color="colorful"
                            style={{ marginTop: "12px" }}
                        />
                        <div
                            style={{
                                borderRadius: "4px",
                                padding: "12px",
                                backgroundColor: tokens.colorNeutralForegroundInverted,
                            }}>
                            <div
                                style={{
                                    display: "flex",
                                    columnGap: "12px",
                                    marginBottom: "2px",
                                }}>
                                <Link>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        Terminator
                                    </Text>
                                </Link>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        columnGap: "4px",
                                        alignItems: "center",
                                    }}>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        15:47
                                    </Text>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>Chill out, dickwad.</Text>
                        </div>
                    </div>

                    <div
                        style={{
                            display: "flex",
                            columnGap: "12px",
                            alignSelf: "start",
                        }}>
                        <Avatar
                            name="John Connor"
                            color="colorful"
                            style={{ marginTop: "12px" }}
                        />
                        <div
                            style={{
                                borderRadius: "4px",
                                padding: "12px",
                                backgroundColor: tokens.colorBrandBackground2,
                            }}>
                            <div
                                style={{
                                    display: "flex",
                                    columnGap: "12px",
                                    marginBottom: "2px",
                                }}>
                                <Link appearance="subtle">
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        John Connor
                                    </Text>
                                </Link>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        columnGap: "4px",
                                        alignItems: "center",
                                    }}>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        15:56
                                    </Text>
                                    <Tooltip
                                        relationship="description"
                                        content="Seen"
                                        hideDelay={0}
                                        showDelay={0}>
                                        <span>
                                            <Eye16Filled
                                                style={{
                                                    display: "block",
                                                    margin: "auto",
                                                    color: tokens.colorNeutralForeground4,
                                                }}
                                            />
                                        </span>
                                    </Tooltip>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>You're getting it!</Text>
                        </div>
                    </div>

                    <div
                        style={{
                            display: "flex",
                            columnGap: "12px",
                            alignSelf: "start",
                        }}>
                        <Avatar
                            name="Terminator"
                            color="colorful"
                            style={{ marginTop: "12px" }}
                        />
                        <div
                            style={{
                                borderRadius: "4px",
                                padding: "12px",
                                backgroundColor: tokens.colorNeutralForegroundInverted,
                            }}>
                            <div
                                style={{
                                    display: "flex",
                                    columnGap: "12px",
                                    marginBottom: "2px",
                                }}>
                                <Link>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        Terminator
                                    </Text>
                                </Link>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        columnGap: "4px",
                                        alignItems: "center",
                                    }}>
                                    <Text
                                        style={{
                                            ...typographyStyles.caption1,
                                            color: tokens.colorNeutralForeground4,
                                        }}>
                                        15:47
                                    </Text>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>No problemo</Text>
                        </div>
                    </div>

                    {/* <div style={{ display: "flex", columnGap: "12px", alignSelf: "start" }}>
                        <Avatar name="John Connor" color="colorful" style={{ marginTop: "12px" }} />
                        <div style={{ borderRadius: "4px", padding: "12px", backgroundColor: tokens.colorBrandBackground2 }}>
                            <div style={{ display: "flex", columnGap: "12px", marginBottom: "2px" }}>
                                <Text style={{ ...typographyStyles.caption1, color: tokens.colorNeutralForeground4 }}>John Connor</Text>
                                <div style={{ display: "flex", flexDirection: "row", columnGap: "4px", alignItems: "center" }}>
                                    <Text style={{ ...typographyStyles.caption1, color: tokens.colorNeutralForeground4 }}>
                                        15:56
                                    </Text>
                                    <Clock16Regular style={{ color: tokens.colorNeutralForeground4 }} />
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>
                                Or 'Later, dickwad'. And if someone gets upset you say, 'chill out'! Or do combinations.
                            </Text>
                        </div>
                    </div> */}

                    {/* <div style={{ display: "flex", columnGap: "12px", alignSelf: "start" }}>
                        <Avatar name="John Connor" color="colorful" style={{ marginTop: "12px" }} />
                        <div style={{ borderRadius: "4px", padding: "12px", backgroundColor: tokens.colorBrandBackground2 }}>
                            <div style={{ display: "flex", columnGap: "12px", marginBottom: "2px" }}>
                                <Text style={{ ...typographyStyles.caption1, color: tokens.colorNeutralForeground4 }}>John Connor</Text>
                                <div style={{ display: "flex", flexDirection: "row", columnGap: "4px", alignItems: "center" }}>
                                    <Text style={{ ...typographyStyles.caption1, color: tokens.colorNeutralForeground4 }}>
                                        15:56
                                    </Text>
                                    <CheckmarkCircle16Regular style={{ color: tokens.colorNeutralForeground4 }} />
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>
                                You're getting it!
                            </Text>
                        </div>
                    </div> */}

                    {/* <div style={{ display: "flex", columnGap: "12px", alignSelf: "start" }}>
                        <Avatar name="T1000" color="colorful" style={{ marginTop: "12px" }} />
                        <div style={{ borderRadius: "4px", padding: "12px", backgroundColor: tokens.colorNeutralForegroundInverted }}>
                            <div style={{ display: "flex", columnGap: "12px", marginBottom: "2px" }}>
                                <Text style={{ ...typographyStyles.caption1, color: tokens.colorNeutralForeground4 }}>T1000</Text>
                                <div style={{ display: "flex", flexDirection: "row", columnGap: "4px", alignItems: "center" }}>
                                    <Text style={{ ...typographyStyles.caption1, color: tokens.colorNeutralForeground4 }}>
                                        15:56
                                    </Text>
                                </div>
                            </div>
                            <Text style={{ userSelect: "text" }}>
                                &gt;:E
                            </Text>
                        </div>
                    </div> */}

                    {/* <Divider style={{ maxHeight: "0", paddingLeft: "44px", marginTop: "24px", marginBottom: "24px" }}>today</Divider> */}
                </div>
            </div>

            <div className={styles.composeBox}>
                <ComposeBox />
            </div>

            {/* <div ref={composeContainerRef} style={{ gridColumnStart: "3", "--scrollbar-width": composeContainerOffset } as CSSProperties}>
                <Textarea
                    spellCheck={true}
                    rows={1}
                    style={{ borderWidth: "1px", borderColor: tokens.colorNeutralStroke2, width: "100%" }}
                    placeholder="Type a message" />
                <div style={{ display: "flex", justifyContent: "space-between" }}>
                    <div>
                        <Button appearance="transparent" icon={<DrawTextIcon />} />
                        <Button appearance="transparent" icon={<AttachIcon />} />
                        <Button appearance="transparent" icon={<EmojiIcon />} />
                        <Button appearance="transparent" icon={<StickerIcon />} />
                    </div>
                    <Tooltip relationship="label" content="Send">
                        <Button appearance="transparent" icon={<SendIcon />} />
                    </Tooltip>
                </div>
            </div> */}
        </div>
    );
};

export default App;
