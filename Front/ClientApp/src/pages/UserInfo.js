import * as React from "react";
import { Component } from "react";
import { email } from "react-admin";


export class UserInfo extends Component {
    static displayName = UserInfo.name;

    constructor(props) {
        super(props);
        this.state = {
            fullname: '',
            mail: '',
            password: '',
            companyname: '',
        }
    }

    const config = {
        ReactSession.get("token", token.data);

        this.setState({ token: token.data, loggedIn: true });
    }

    populateData = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'http://localhost:4388/api/v1/user/UserId',
            data: { , password }
        }).then(function (data) {
            console.log(data);
            self.setState(data);
        });
    }

    render() {
        <body>
            <h1>User</h1>
            <div className="py-2">
                <Label for="fullname">Full name</Label>
                <Input type="text" name="fullname" value={full_name}/>
            </div>
            <div className="py-2">
                <Label for="email">Email</Label>
                <Input type="text" name="email" value={mail} />
            </div>
            <div className="py-2">
                <Label for="password">Password</Label>
                <Input type="password" name="password" value={password}/>
            </div>
        </body>
    }
}