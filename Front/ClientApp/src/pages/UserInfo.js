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
            message:0,
            jtoken: localStorage.getItem("token")
        }
        this.OnLoad = this.OnLoad.bind(this)
        this.OnUpdateUserData = this.OnUpdateUserData.bind(this)
    }

    componentDidMount() {
        this.OnLoad();
    }

    componentDidUpdate() {
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
        const user = self.state.userData
        const company = self.state.companyData
        axios({
            method: 'POST',
            url: process.env.REACT_APP_API_BACKEND + '/api/v1/user/UpdateData',
            dataType: "json",
            data: { user, company }
        }).then((data) => {
            self.setState({ message: 2 }, () => { console.log(self.state.message) })
            return;
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
                {
                    this.state.message == 2 &&
                    <>
                        < div class="alert alert-success" >
                            <strong>Success!</strong> It worked, the data has been updated!!
                        </div >
                    </>
                }
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
                        {this.state.companyData.id != 0 &&
                            <>
                                <h3> Company info</h3>
                                <div className="py-2">
                                    <Label for="companyName">Company Name</Label>
                                    <Input type="companyName" placeholder={this.state.companyData.companyName} onChange={(e) => this.state.companyData.companyName = e.target.value} name="companyName" />
                                </div>
                                <div className="py-2">
                                    <Label for="PhoneNumber">Company Phone Number</Label>
                                    <Input type="PhoneNumber" placeholder={this.state.companyData.phoneNumber} onChange={(e) => this.state.companyData.phoneNumber = e.target.value} name="PhoneNumber" />
                                </div>
                                <div className="py-2">
                                    <div className="py-2">
                                        <Label for="Street">Street</Label>
                                        <Input type="Street" placeholder={this.state.companyData.street} onChange={(e) => this.state.companyData.street = e.target.value} name="Street" />
                                    </div>
                                    <div className="py-2">
                                        <Label for="HouseNumber">HouseNumber</Label>
                                        <Input type="HouseNumber" placeholder={this.state.companyData.houseNumber} onChange={(e) => this.state.companyData.houseNumber = e.target.value} name="HouseNumber" />
                                    </div>
                                    <div className="py-2">
                                        <Label for="Zipcode">Zipcode</Label>
                                        <Input type="Zipcode" placeholder={this.state.companyData.zipCode} onChange={(e) => this.state.companyData.zipCode = e.target.value} name="Zipcode" />
                                    </div>
                                </div>
                            </>
                        }
                        <Button className="my-2 mr-2 ml-0 loginbutton" onClick={(e) => this.OnUpdateUserData(e)}>Update</Button>
                    </div>
                </div>
            </body >
        )
    }
}