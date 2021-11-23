import * as React from "react";
import { Component } from "react";
import { email } from "react-admin";
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import axios from 'axios';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Link, Redirect } from 'react-router-dom';

const config = {
    headers: {
        Authorization: localStorage.getItem('token')
    }
}

export class UserInfo extends Component {
    static displayName = UserInfo.name;

    constructor(props) {
        super(props);
        this.state = {
            fullname: '',
            mail: '',
            password: '',
            companyname: '',
            loggedIn: false,
            token: null
        }
    }

    populateData = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'http://localhost:4388/api/v1/user/GetUserById',
            data: { config }
        }).then(function (data) {
            console.log(data);
            self.setState(data);
        });
    }

    render() {
        console.log(config);
/*        if (!localStorage.get("loggedin")) {
            return (
                <Redirect to="/user/dashboard/" />
            )

        }*/
        return (
            <body>
                <h1>User</h1>
                <div className="py-2">
                    <Label for="fullname">Full name</Label>
                    {/*      <Input type="text" name="fullname"/>*/}
                </div>
                <div className="py-2">
                    <Label for="email">Email</Label>
                    {/*         <Input type="text" name="email"/>*/}
                </div>
                <div className="py-2">
                    <Label for="password">Password</Label>
                    {/*          <Input type="password" name="password"/>*/}
                </div>
            </body>
        )
    }
}