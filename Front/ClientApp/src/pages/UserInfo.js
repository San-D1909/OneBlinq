import * as React from "react";
import { Component } from "react";
import { NavMenu } from '../components/NavMenu';
import { email } from "react-admin";
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import axios from 'axios';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Link, Redirect } from 'react-router-dom';
import jwt from 'jwt-decode'
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardImg from 'reactstrap/lib/CardImg';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';

export class UserInfo extends Component {
    static displayName = UserInfo.name;

    constructor(props) {
        super(props);

        this.state = {
            userData: [],
            companyData: [],
            loggedIn: false,
            jtoken: localStorage.getItem("token")
        }
        this.OnLoad = this.OnLoad.bind(this)
        this.OnUpdateUserData = this.OnUpdateUserData.bind(this)
    }

    componentDidMount() {
        this.OnLoad();
    }

    OnLoad = (e) => {
        var self = this;
        axios({
            method: 'GET',
            url: process.env.REACT_APP_API_BACKEND + '/api/v1/user/GetUserByToken',
            params: {
                jtoken: localStorage.getItem("token"),
            }
        }).then((data) => {
            self.setState({ userData: data.data.userById }, () => { console.log(self.state.userData) });
            self.setState({ companyData: data.data.userCompany }, () => { console.log(self.state.companyData) });
        });
    }
    OnUpdateUserData = (e) => {
        var self = this;
        const userid = self.state.userData.userId
        const user = self.state.userData
        const company = self.state.companyData

        console.log(userid.id);
        axios({
            method: 'POST',
            url: process.env.REACT_APP_API_BACKEND + '/api/v1/user/UpdateData/' + userid.id + "/",
            dataType: "json",
            data: {userid, user, company}
        }).then((data) => {

        });
    }



    render() {
        if (!localStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        return (
            <body>
                <NavMenu />
                <div class="card mx-auto" style={{ backgroundColor: "white", maxWidth: "60%" }}>
                    <div class="card-body">
                        <h4 style={{ textAlign: "center", fontWeight: "bold" }} class="card-title">UserData</h4>
                        <h6 style={{ textAlign: "center" }}>You can update your userdata in the fields below.</h6>
                        <hr class="solid"></hr>
                        <div className="py-2">
                            <Label for="fullName">Full Name</Label>
                            <Input type="fullName" placeholder={this.state.userData.fullName} onChange={(e) => this.state.userData.fullName = e.target.value} name="fullName" />
                        </div>
                        <div className="py-2">
                            <Label for="email">Email</Label>
                            <Input type="email" placeholder={this.state.userData.email} onChange={(e) => this.state.userData.email = e.target.value} name="email" />
                        </div>
                        <Button className="my-2 mr-2 ml-0 loginbutton" onClick={(e) => this.OnUpdateUserData(e)}>Update</Button>
                    </div>
                </div>
            </body >
        )
    }
}