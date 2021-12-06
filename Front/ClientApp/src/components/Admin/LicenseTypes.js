// in src/posts.js
import * as React from "react";
import { List, Datagrid, TextField, DateField, BooleanField, ImageInput, TranslatableInputs, ImageField, NumberInput, TranslatableFields, Show, SimpleShowLayout, RichTextField, NumberField, ShowButton, TabbedShowLayout, Tab, ReferenceArrayField, ArrayField, email, EmailField } from 'react-admin';
import { Create, Edit, SimpleForm, TextInput, DateInput, ReferenceManyField, EditButton, required } from 'react-admin';
import RichTextInput from 'ra-input-rich-text';

export const LicenseTypeList = (props) => (
    <List {...props} >
        <Datagrid>
            <TextField source="typeName" label="Name" />
            <NumberField source="maxAmount" label="Max amount" />
            <NumberField source="monthlyPeriod" label="Monthly period" />
            <LicenseTypeShowButton {...props} />
        </Datagrid>
    </List>
);


export const LicenseTypeCreate = (props) => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="typeName" label="Name" validate={required()} />
            <NumberInput source="maxAmount" label="Max amount" validate={required()} />
            <NumberInput source="monthlyPeriod" label="Monthly period" validate={required()} />
        </SimpleForm>
    </Create>
);

export const LicenseTypeEdit = (props) => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="typeName" label="Name" validate={required()} />
            <NumberInput source="maxAmount" label="Max amount" validate={required()} />
            <NumberInput source="monthlyPeriod" label="Monthly period" validate={required()} />
        </SimpleForm>
    </Edit>
);

const LicenseTypeShowButton = ({ record }) => {
    return (
    <ShowButton basePath="plugin" label="Show info" record={ record } />
        );
    
}

export const LicenseTypeShow = (props) => {
    return (
        <Show {...props}>
            <SimpleShowLayout>
                <TextField source="typeName" label="Name" />
                <NumberField source="maxAmount" label="Max amount" />
                <NumberField source="monthlyPeriod" label="Monthly period" />
            </SimpleShowLayout>
        </Show>
    );
}