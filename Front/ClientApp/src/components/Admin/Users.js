// in src/posts.js
import * as React from "react";
import { List, Datagrid, TextField, DateField, BooleanField, ImageInput, TranslatableInputs, ImageField, NumberInput, TranslatableFields, Show, SimpleShowLayout, RichTextField, NumberField, ShowButton } from 'react-admin';
import { Create, Edit, SimpleForm, TextInput, DateInput, ReferenceManyField, EditButton, required } from 'react-admin';
import RichTextInput from 'ra-input-rich-text';

const UserFilters = [
    <TextInput label="Plugin name" source="pluginName" />,
    <TextInput label="Plugin description" source="pluginDescription" />,
];

const UserPanel = ({ id, record, resource }) => {
    return (<div dangerouslySetInnerHTML={{
        __html: record.pluginDescription
    }}
    />)
}

export const UserList = (props) => (
    <List {...props} filters={UserFilters}>
        <Datagrid expand={<UserPanel />}>
            <TextField source="userEmail" label="User Email" />
            <UserShowButton {...props} />
        </Datagrid>
    </List>
);


export const UserCreate = (props) => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="userEmail" label="User Email" validate={required()} />
            <TextInput source="userFullname" label="User Full Name" validate={required()} />
        </SimpleForm>
    </Create>
);

export const UserEdit = (props) => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="pluginName" label="Plugin name" validate={required()} />
            <RichTextInput source="pluginDescription" label="Plugin description" validate={required()} />
            <NumberInput source="price" label="Price" validate={required()} />
        </SimpleForm>
    </Edit>
);

const UserShowButton = ({ record }) => {
    return (
        <ShowButton basePath="plugins" label="Show info" record={record} />
    );

}

export const UserShow = (props) => (
    <Show {...props}>
        <SimpleShowLayout>
            <TextField source="pluginName" label="Plugin name" />
            <RichTextField source="pluginDescription" label="Plugin description" />
            <NumberField source="price" label="Price" />
        </SimpleShowLayout>
    </Show>
);