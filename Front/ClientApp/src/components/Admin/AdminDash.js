import * as React from "react";
import { Admin, Resource, ListGuesser,EditGuesser} from 'react-admin';
import jsonServerProvider from 'ra-data-json-server';
import { createMuiTheme } from '@material-ui/core/styles';
import { ThemeOptions } from '@material-ui/core';

export const newOptions: ThemeOptions = {

    // theme customizable at https://bareynol.github.io/mui-theme-creator

    palette: {
        type: 'dark',
        primary: {
            main: '#b53f41',
        },
        secondary: {
            main: '#f50057',
        },
        background: {
            default: '#000000',
            paper: '#2f2f2f',
        },
    },
};

const dataProvider = jsonServerProvider('http://jsonplaceholder.typicode.com');

const theme = createMuiTheme({
    palette: {
        type: 'dark', // Switching the dark mode on is a single property value change.
    },
});

const AdminDash = () => (
    <Admin theme={newOptions} dataProvider={dataProvider}>
        <Resource name="users"
            list={ListGuesser}
            edit={EditGuesser}
        />
    </Admin>
);

export default AdminDash;