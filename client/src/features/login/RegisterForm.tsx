import { ErrorMessage, Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Button, Header, Label } from "semantic-ui-react";
import CustomTextInput from "../../app/common/form/customTextInput";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';
import ValidationErrors from "../../app/errors/ValidationErrors";



export default observer(function RegisterForm() {
    const { accountStore } = useStore();
    return (
        <Formik
            initialValues={{ employeeNo: "", name: "", mail: "", password: "", error: null }}
            onSubmit={(values, { setErrors }) => accountStore.register(values)
                .catch(error => setErrors({ error }))
            }
            validationSchema={Yup.object({
                displayName: Yup.string().required(),
                username: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required(),

            })}
        >
            {({ handleSubmit, isSubmitting, errors, isValid, dirty }) => (
                <Form className="ui form error" onSubmit={handleSubmit} autoComplete="off">
                    <Header as="h2" content="Sign up for Activities" color="red" textAlign="center" />
                    <CustomTextInput name="name" placeholder="Name" />
                    <CustomTextInput name="employeeNo" placeholder="Employee No" />
                    <CustomTextInput name="mail" placeholder="Mail" />
                    <CustomTextInput name="password" placeholder="Password" type="password" />
                    <Button disabled={!isValid || !dirty || isSubmitting} loading={isSubmitting} positive content="Register" type="submit" fluid />
                    <ErrorMessage
                        name="error" render={() =>
                            <ValidationErrors errors={errors.error} />}
                    />

                </Form>

            )}
        </Formik>
    )
})