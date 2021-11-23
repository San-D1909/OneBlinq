// in src/posts.js
import * as React from "react";
import { List, Datagrid, TextField, DateField, BooleanField, ImageInput, TranslatableInputs, ImageField, NumberInput, TranslatableFields, Show, SimpleShowLayout, RichTextField, NumberField, ShowButton } from 'react-admin';
import { Create, Edit, SimpleForm, TextInput, DateInput, ReferenceManyField, EditButton, required } from 'react-admin';
import RichTextInput from 'ra-input-rich-text';

export const LicenseList = (props) => (
    <List {...props}>
        <Datagrid>
            <TextField source="licensekey" label="License key"/>
            <TextField source="plugin.pluginname" label="Plugin name" />
            <TextField source="limit" label="Limit" />
            <TextField source="amountactivated" label="Amount activated" />
            <DateField source="creationtime" label="Created on" />
            <DateField source="expirationdate" label="Expires on" />
            <LicenseShowButton {...props} />
        </Datagrid>
    </List>
);

const LicenseShowButton = ({ record }) => {
    console.log(record);
    return (
    <ShowButton basePath="licenses" label="Show info" record={ record } />
        );
    
}

export const LicenseShow = (props) => (
    <Show {...props}>
        <SimpleShowLayout>
            <TextField source="licensekey" label="License key" />
            <TextField source="plugin.pluginname" label="Plugin name" />
            <TextField source="limit" label="Limit" />
            <TextField source="amountactivated" label="Amount activated" />
            <DateField source="creationtime" label="Created on" />
            <DateField source="expirationdate" label="Expires on" />
        </SimpleShowLayout>
    </Show>
);