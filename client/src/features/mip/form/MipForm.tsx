import { observer } from "mobx-react-lite";
import react, { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { NavLink, useHistory, useParams } from 'react-router-dom';
import LoadingComponent from "../../../app/layout/LoadingComponents";
import { v4 as uuid } from 'uuid';
import { Formik, Form } from "formik";
import * as Yup from "yup";
import CustomTextInput from "../../../app/common/form/customTextInput";
import CustomTextArea from '../../../app/common/form/customTextArea';
import CustomSelectedTextInput from '../../../app/common/form/customSelectedTextInput';
import { categoryOptions } from '../../../app/common/options/categoryOptions';
import CustomDateInput from '../../../app/common/form/customDateInput';
import { ActivityFormValues } from '../../../app/models/activity';
import { MipFormValues } from "../../../app/models/mip";
import ToastHelper from "../../../app/helpers/ToastHelper";
import { caridemodeOptions } from "../../../app/common/options/caridemodeOptions";



export default observer(function MipForm() {
    const history = useHistory();

    const { mipStore } = useStore();
    const { createMip, updateMip, loadMip, loadingInitial, setReturnModel } = mipStore;
    const { id } = useParams<{ id: string }>();



    const [mip, setMip] = useState<MipFormValues>(new MipFormValues());



    const validationSchema = Yup.object({
        code: Yup.string().required("Kod kısmı boş geçilemez"),
        cd: Yup.string().required("Cari/Demode kısmı boş geçilemez"),
    });


    //explanation mark means it can be undefined
    useEffect(() => {
        if (id) loadMip(id).then(mip => setMip(new MipFormValues(mip)))
    }, [id, loadMip]);



    function handleFormSubmit(mip: MipFormValues) {
        if (!mip.id) {
            let newMip = {
                ...mip,
                id: uuid()
            };
            createMip(newMip).then((mipReturnModel) => {
                setReturnModel(mipReturnModel);
                if (mipReturnModel?.status === 1)
                    history.push('/mips');
                else
                    ToastHelper.ErrorToast(mipReturnModel?.message)
            })
        } else {
            updateMip(mip).then((mipReturnModel) => {
                setReturnModel(mipReturnModel);
                if (mipReturnModel?.status === 1)
                    history.push('/mips');
                else
                    ToastHelper.ErrorToast(mipReturnModel?.message)
            })
        }
    }


    if (loadingInitial) return <LoadingComponent content="Loading activity.Please Wait..." />

    return (
        <Segment clearing>
            <Header content={"Mip"} size="huge" sub color="black" />
            <br></br>
            <Formik
                enableReinitialize
                initialValues={mip}
                onSubmit={values => handleFormSubmit(values)}
                validationSchema={validationSchema}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <CustomSelectedTextInput options={caridemodeOptions} placeholder="Cari/Demode" name="cd" />
                        <CustomTextInput name="code" placeholder="Mip Kodu" />
                        {/* <CustomTextInput name="cd" placeholder="Cari-Demode" />DropDown olacak Cari-Demode */}

                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting}
                            floated="right"
                            positive type="submit"
                            content="Kaydet" />
                        <Button

                            as={NavLink} to="/mips"
                            floated="right"
                            type="button"
                            content="İptal" />
                    </Form>
                )}
            </Formik>


        </Segment>
    )
})