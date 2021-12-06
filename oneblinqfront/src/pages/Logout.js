import React, { Component, useEffect } from 'react';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Redirect } from 'react-router-dom';

export class Logout extends Component {
    static displayName = Logout.name;

    render() {

        if (localStorage.getItem("loggedin")) {
            localStorage.clear()

            return (
                <Redirect to="/login" />
            )
        }
     }
}

