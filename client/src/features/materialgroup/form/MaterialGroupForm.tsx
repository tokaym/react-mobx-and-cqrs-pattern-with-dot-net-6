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
import { MaterialGroupFormValues } from "../../../app/models/materialgroup";
import ToastHelper from "../../../app/helpers/ToastHelper";



export default observer(function MaterialGroupForm() {
    const history = useHistory();

    const { materialgroupStore } = useStore();
    const { createMaterialGroup, updateMaterialGroup, loadMaterialGroup, loadingInitial,setReturnModel } = materialgroupStore;
    const { id } = useParams<{ id: string }>();



    const [materialGroup, setMaterialGroup] = useState<MaterialGroupFormValues>(new MaterialGroupFormValues());



    const validationSchema = Yup.object({
        materialSKU: Yup.string().required("Malzeme SKU boş olamaz"),
        groupName: Yup.string().required("Grup adı boş olamaz"),
    });


    //explanation mark means it can be undefined
    useEffect(() => {
        if (id) loadMaterialGroup(id).then(materialGroup => setMaterialGroup(new MaterialGroupFormValues(materialGroup)))
    }, [id, loadMaterialGroup]);



    function handleFormSubmit(materialGroup: MaterialGroupFormValues) {
        if (!materialGroup.id) {
            let newMaterialGroup = {
                ...materialGroup,
                id: uuid()
            };
            createMaterialGroup(newMaterialGroup).then((materialGroupReturnModel) => {
                setReturnModel(materialGroupReturnModel);
                if (materialGroupReturnModel?.status === 1)
                    history.push('/materialgroups');
                else
                    ToastHelper.ErrorToast(materialGroupReturnModel?.message + " " + materialGroupReturnModel?.errorMessage)
            })
        } else {
            updateMaterialGroup(materialGroup).then((materialGroupReturnModel) => {
                setReturnModel(materialGroupReturnModel);
                if (materialGroupReturnModel?.status === 1)
                    history.push('/materialgroups');
                else
                    ToastHelper.ErrorToast(materialGroupReturnModel?.message + " " + materialGroupReturnModel?.errorMessage)
            })
        }
    }


    if (loadingInitial) return <LoadingComponent content="Loading activity.Please Wait..." />

    return (
        <Segment clearing>
            <Header content={"Malzeme Grubu"} size="huge" sub color="black" />
            <br></br>
            <Formik
                enableReinitialize
                initialValues={materialGroup}
                onSubmit={values => handleFormSubmit(values)}
                validationSchema={validationSchema}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <CustomTextInput name="materialSKU" placeholder="Malzeme SKU" />
                        <CustomTextInput name="groupName" placeholder="Grup Adı" />


                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting}
                            floated="right"
                            positive type="submit"
                            content="Kaydet" />
                        <Button

                            as={NavLink} to="/materialgroups"
                            floated="right"
                            type="button"
                            content="İptal" />
                    </Form>
                )}
            </Formik>


        </Segment>
    )
})