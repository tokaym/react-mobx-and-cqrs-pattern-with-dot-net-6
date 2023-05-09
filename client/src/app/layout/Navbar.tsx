import React from "react";
import { Link, NavLink } from "react-router-dom";
import { Button, Container, Menu, Image, Dropdown } from "semantic-ui-react";
import "../layout/sytles.css";
import UserStore from '../stores/accountStore';
import { useStore } from '../stores/store';
import { observer } from "mobx-react-lite";


export default observer(function Navbar() {
    const { accountStore: { user, logout } } = useStore();
    return (
        <Menu inverted={true} fixed="top">
            <Container>
                <Menu.Item as={NavLink} to="/" exact header>
                    <img src="assets/arlogo.png" alt="logo" style={{ marginRight: "10px" }} />
                    Activities
                </Menu.Item>
                {/* <Menu.Item position="right">
                    <Image src={"/assets/user.jpeg"} avatar spaced="right" />
                    <Dropdown pointing="top left" text={user?.employeeNo} >
                        <Dropdown.Menu>
                            <Dropdown.Item
                                as={Link} to={`profiles/${user?.employeeNo}`}
                                text="Profile Settings"
                                icon="user" />
                            <Dropdown.Item
                                onClick={logout}
                                text="Logout"
                                icon="power" />
                        </Dropdown.Menu>
                    </Dropdown>

                </Menu.Item> */}
            </Container>
        </Menu>
    )
})