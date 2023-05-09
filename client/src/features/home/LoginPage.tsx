import { observer } from "mobx-react-lite";
import React from "react";
import { Link } from "react-router-dom";
import { Button, Container, Header, Image, Segment } from 'semantic-ui-react';
import { useStore } from "../../app/stores/store";
import LoginForm from "../login/LoginForm";
import RegisterForm from "../login/RegisterForm";

export default observer(function LoginPage() {
    const { accountStore, modalStore } = useStore();


    return (
        <Segment inverted={true} textAlign="center" vertical className="masthead">
            <Container text>
                <Header as="h1" inverted={true}>
                    {/* <Image size="massive" src="/assets/spare-icon.png" alt="logo" style={{ marginBottom: 10 }} /> */}
                    <br />Spare Part Management System
                </Header>
                <>
                    <Button onClick={() => modalStore.openModal(<LoginForm />)} size="huge" inverted={true}>
                        Giriş Yap
                    </Button>
                    {/* <Button onClick={() => modalStore.openModal(<RegisterForm />)} size="huge" inverted>
                            Register
                        </Button> */}
                </>


            </Container>

        </Segment>
    )
})