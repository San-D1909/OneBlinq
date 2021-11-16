// in src/posts.js
import * as React from "react";
import { List, Datagrid, TextField, DateField, BooleanField, ImageInput, TranslatableInputs, ImageField, NumberInput, TranslatableFields, Show, SimpleShowLayout, RichTextField, NumberField, ShowButton } from 'react-admin';
import { Create, Edit, SimpleForm, TextInput, DateInput, ReferenceManyField, EditButton, required } from 'react-admin';
import RichTextInput from 'ra-input-rich-text';

export const PluginList = (props) => (
    <List {...props}>
        <Datagrid>
            <TextField source="pluginname.nl" label="Plugin Name (NL)"/>
            <TextField source="pluginname.en" label="Plugin Name (EN)"/>
            <TextField source="price" />
            <PluginShowButton {...props}/>
        </Datagrid>
    </List>
);


export const PluginCreate = (props) => (
    <Create {...props}>
        <SimpleForm>
            <TranslatableInputs locales={['en', 'nl']} required>
                <TextInput source="pluginname" label="Plugin Name" validate={required()} />
                <RichTextInput source="plugindescription" label="Plugin Description" validate={required()} />
            </TranslatableInputs>
            <ImageInput source="pictures" label="Picture" accept="image/*" placeholder={<p>Drop your file here</p>} validate={required()} >
                <ImageField source="src" title="title" />
            </ImageInput>
            <NumberInput source="price" validate={required()} />
        </SimpleForm>
    </Create>
);

export const PluginEdit = (props) => (
    <Edit {...props}>
        <SimpleForm>
            <TranslatableInputs locales={['en', 'nl']}>
                <TextInput source="pluginname" label="Plugin Name" validate={required()} />
                <RichTextInput source="plugindescription" label="Plugin Description" validate={required()} />
            </TranslatableInputs>
            <ImageInput source="pictures" label="Picture" accept="image/*" placeholder={<p>Drop your file here</p>} validate={required()}>
                <ImageField source="src" title="title" />
            </ImageInput>
            <NumberInput source="price" label="Price" validate={required()} />
        </SimpleForm>
    </Edit>
);

const PluginShowButton = ({ record }) => {
    return (
    <ShowButton basePath="plugins" label="Show info" record={ record } />
        );
    
}

export const PluginShow = (props) => (
    <Show {...props}>
        <SimpleShowLayout>
            <TranslatableFields locales={['en', 'nl']}>
                <TextField source="pluginname" />
                <RichTextField source="plugindescription" />
            </TranslatableFields>
            <ImageField source="pictures.src" label="Picture" title="title" />
            <NumberField source="price" label="Price" />
        </SimpleShowLayout>
    </Show>
);