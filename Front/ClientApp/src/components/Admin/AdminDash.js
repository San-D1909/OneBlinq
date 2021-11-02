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
            main: '#6e44ff',
            contrastText: '#deeff4',
            light: '#efa9ae',
            dark: '#efa9ae',
        },
        secondary: {
            main: '#efa9ae',
        },
        background: {
            default: '#02021e',
            paper: '#1b1d33',
        },
        text: {
            primary: '#edeffc',
            secondary: '#d6d8e5',
            disabled: '#c1c3d0',
            hint: '#9ea0ac',
        },
        info: {
            main: '#efa9ae',
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