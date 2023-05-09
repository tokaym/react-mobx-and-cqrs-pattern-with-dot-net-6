import { observer } from "mobx-react-lite";
import react, { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { NavLink, useHistory, useParams } from 'react-router-dom';
import LoadingComponent from "../../app/layout/LoadingComponents";
import { v4 as uuid } from 'uuid';
import { Formik, Form } from "formik";
import * as Yup from "yup";
import CustomTextInput from "../../app/common/form/customTextInput";
import { MaterialGroupFormValues } from "../../app/models/materialgroup";
import ToastHelper from "../../app/helpers/ToastHelper";
import { MailSettingFormValues } from "../../app/models/mailsetting";



export default observer(function MailSettingForm() {
    const history = useHistory();

    const { mailSettingStore } = useStore();
    const { updateMailSettings, loadMailSettings, loadingInitial, setReturnModel } = mailSettingStore;



    const [mailSettings, setMailSettings] = useState<MailSettingFormValues>(new MailSettingFormValues());



    const validationSchema = Yup.object({
        id: Yup.string().required("ID bilgisi boş olamaz"),
        name: Yup.string().required("Kod boş olamaz"),
        to: Yup.string().required("To boş olamaz"),
        cc: Yup.string().required("Cc boş olamaz"),
        from: Yup.string().required("From boş olamaz"),
    });


    //explanation mark means it can be undefined
    useEffect(() => {
        loadMailSettings().then(mailSettings => setMailSettings(new MailSettingFormValues(mailSettings)))
    }, [1, loadMailSettings]);



    function handleFormSubmit(mailSettings: MailSettingFormValues, actions: any) {
        updateMailSettings(mailSettings).then((materialGroupReturnModel) => {
            setReturnModel(materialGroupReturnModel);
            setTimeout(() => {
                actions.setSubmitting(false);
            }, 1000);
        })

    }


    if (loadingInitial) return <LoadingComponent content="Loading activity.Please Wait..." />

    return (
        <Segment clearing>
            <Header content={"Email Ayarları"} size="huge" sub color="black" />
            <br></br>
            <Formik
                enableReinitialize
                initialValues={mailSettings}
                onSubmit={(values, actions) => handleFormSubmit(values, actions)}
                validationSchema={validationSchema}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <CustomTextInput label="ID" disabled name="id" placeholder="Id" />
                        <CustomTextInput label="Kod" disabled name="name" placeholder="Ayar Kodu" />
                        <CustomTextInput label="To" name="to" placeholder="To" />
                        <CustomTextInput label="Cc" name="cc" placeholder="Cc" />
                        <CustomTextInput label="From" disabled name="from" placeholder="From" />



                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting}
                            floated="right"
                            positive type="submit"
                            content="Kaydet" />
                        <Button

                            as={NavLink} to="/home"
                            floated="right"
                            type="button"
                            content="İptal" />
                    </Form>
                )}
            </Formik>


        </Segment>
    )
})