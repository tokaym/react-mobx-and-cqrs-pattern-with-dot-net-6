import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Button, Divider, Dropdown, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { NavLink, useHistory, useParams } from 'react-router-dom';
import LoadingComponent from "../../../app/layout/LoadingComponents";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import CustomTextInput from "../../../app/common/form/customTextInput";
import { UserUpdateFormValues } from "../../../app/models/user";
import ToastHelper from "../../../app/helpers/ToastHelper";
import { action } from "mobx";
import CustomMultipleSelectedTextInput from "../../../app/common/form/customMultipleSelectedTextInput";



export default observer(function UserForm() {
    const history = useHistory();

    const { userStore } = useStore();
    const { updateUser, loadUser, loadingInitial, setReturnModel, dropdownOptions } = userStore;
    const { id } = useParams<{ id: string }>();



    const [user, setUser] = useState<UserUpdateFormValues>(new UserUpdateFormValues());



    const validationSchema = Yup.object({
        employeeNo: Yup.string().required("Sicil boş geçilemez"),
        name: Yup.string().required("İsim boş geçilemez"),
        surname: Yup.string().required("Soyad boş geçilemez"),
        mail: Yup.string().required("Email kısmı boş geçilemez").email("Email geçerli formatta giriniz"),
        operationClaimIds: Yup.array()
    });

    //explanation mark means it can be undefined
    useEffect(() => {
        if (id) loadUser(id).then(user => setUser(new UserUpdateFormValues(user)))
    }, [id, loadUser]);



    function handleFormSubmit(user: UserUpdateFormValues, setSubmitting: any) {
        if (!user.id) {
            history.push('/users');
            ToastHelper.ErrorToast("Kullanıcı seçimi yapmanız gerekiyor!")
        }
        else {
            updateUser(user).then((userReturnModel) => {
                setReturnModel(userReturnModel);
                if (userReturnModel?.status !== 1)
                    ToastHelper.ErrorToast(userReturnModel?.message)
                else {
                    setSubmitting(false);
                }
            })
        }
    }


    if (loadingInitial) return <LoadingComponent content="Kullanıcı işlemleri sayfası yükleniyor.Lütfen bekleyiniz..." />

    return (
        <Segment style={{ height: window.innerHeight * 0.75 }} clearing>
            <Header dividing content={"Kullanıcı Bilgileri"} size="huge" sub color="black" />
            <br></br>
            <Formik
                enableReinitialize
                initialValues={user}
                onSubmit={(values, { setSubmitting }) => {
                    handleFormSubmit(values, setSubmitting);
                }}
                validationSchema={validationSchema}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <CustomTextInput name="employeeNo" placeholder="Sicil" disabled />
                        <CustomTextInput name="name" placeholder="İsim" />
                        <CustomTextInput name="surname" placeholder="Soyisim" />
                        <CustomTextInput name="mail" placeholder="Mail" />


                        <br></br>
                        <Header dividing content={"Yetki"} size="huge" sub color="black" />

                        <br></br>
                        <CustomMultipleSelectedTextInput
                            name="operationClaimIds"
                            placeholder="Yetkiler"
                            fluid={true}
                            search={true}
                            selection={true}
                            options={dropdownOptions}
                            // onChange={(event,data) => operationDropdownChange(event,data)}
                        />
                        <br></br>
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting}
                            floated="right"
                            positive type="submit"
                            content="Kaydet" />
                        <Button
                            as={NavLink} to="/users"
                            floated="right"
                            type="button"
                            content="Geri" />
                    </Form>
                )}
            </Formik>


        </Segment>
    )
})