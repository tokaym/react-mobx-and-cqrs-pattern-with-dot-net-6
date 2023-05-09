import { useField } from "formik";
import React from "react";
import { Dropdown, DropdownProps, Form, Label, Select } from "semantic-ui-react";

interface Props {
    name: string;
    options: any,
    label?: string;
    fluid?: boolean;
    multiple?: boolean;
    search?: boolean;
    selection?: any;
    value?: any;
    placeholder:string;
}

export default function CustomMultipleSelectedTextInput(props: Props) {
    const [field, meta, helpers] = useField(props.name);
    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{props.label}</label>
            <Dropdown
                options={props.options}
                value={field.value}
                onChange={(event, data) => helpers.setValue(data.value)}
                onBlur={() => helpers.setTouched(true)}
                fluid={props.fluid}
                multiple={true}
                placeholder={props.placeholder}
                search={props.search}
                selection={props.selection}
            />
            {
                meta.touched && meta.error
                    ?
                    <Label basic color="red" > {meta.error}</Label>
                    :
                    null
            }

        </Form.Field>
    )
}