import React, { Component, useEffect } from 'react';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Redirect } from 'react-router-dom';


export class Logout extends Component {
    static displayName = Logout.name;

    render() {

        if (ReactSession.get("loggedin")) {
            ReactSession.remove("loggedin");
            ReactSession.remove("token");

            return (
                <Redirect to="/login" />
            )
        }
     }
}
