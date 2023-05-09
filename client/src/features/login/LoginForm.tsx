import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header, Label } from "semantic-ui-react";
import CustomTextInput from "../../app/common/form/customTextInput";
import ValidationErrors from "../../app/errors/ValidationErrors";
import { useStore } from "../../app/stores/store";

export default observer(function LoginForm() {
    const { accountStore } = useStore();
    return (
        <Formik
            initialValues={{ mail: "", employeeNo: "", accessToken: "",password:"",name:"", error: null }}
            onSubmit={(values, { setErrors }) => accountStore.login(values)
                .catch(error => setErrors({ error }))
            }
        >
            {({ handleSubmit, isSubmitting, errors }) => (
                <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                    <Header as="h2" content="Giriş Yap" color="black" textAlign="center" />
                    <CustomTextInput name="employeeNo" placeholder="Sicil No" />
                    <CustomTextInput name="password" placeholder="Şifre" type="password" />
                    <Button loading={isSubmitting} positive content="Login" type="submit" fluid />
                    <ErrorMessage
                        name="error" render={() =>
                            <ValidationErrors errors={errors.error} />}
                    />
                </Form>

            )}
        </Formik>
    )
})