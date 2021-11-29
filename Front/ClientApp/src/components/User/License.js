// in src/posts.js
import * as React from "react";
import { List, Datagrid, TextField, DateField, BooleanField, ImageInput, TranslatableInputs, ImageField, NumberInput, TranslatableFields, Show, SimpleShowLayout, RichTextField, NumberField, ShowButton } from 'react-admin';
import { Create, Edit, SimpleForm, TextInput, DateInput, ReferenceManyField, EditButton, required } from 'react-admin';
import RichTextInput from 'ra-input-rich-text';

export const LicenseList = (props) => (
    <List {...props}>
        <Datagrid>
            <TextField source="license.licenseKey" label="License key"/>
            <TextField source="plugin.pluginName" label="Plugin name" />
            <TextField source="license.licenseType.maxAmount" label="Limit" />
            <TextField source="timesActivated" label="Amount activated" />
            <DateField source="license.creationTime" label="Created on" />
            <DateField source="license.expirationTime" label="Expires on" />
            <LicenseShowButton {...props} />
        </Datagrid>
    </List>
);

const LicenseShowButton = ({ record }) => {
    console.log(record);
    return (
    <ShowButton basePath="license" label="Show info" record={ record } />
        );
    
}

export const LicenseShow = (props) => (
    <Show {...props}>
        <SimpleShowLayout>
            <TextField source="license.licenseKey" label="License key" />
            <TextField source="plugin.pluginName" label="Plugin name" />
            <TextField source="license.licenseType.maxAmount" label="Limit" />
            <TextField source="timesActivated" label="Amount activated" />
            <DateField source="license.creationTime" label="Created on" />
            <DateField source="license.expirationTime" label="Expires on" />
        </SimpleShowLayout>
    </Show>
);