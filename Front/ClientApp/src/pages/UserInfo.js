﻿import * as React from "react";
import { Component } from "react";
import { email } from "react-admin";
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import axios from 'axios';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Link, Redirect } from 'react-router-dom';
import { data } from "jquery";


export class UserInfo extends Component {
    static displayName = UserInfo.name;

    constructor(props) {
        super(props);

        this.state = {
            fullname: '',
            mail: '',
            password: '',
            companyname: '',
            userData: [],
            loggedIn: false,
            jtoken: localStorage.getItem("token")
        }
        this.OnLoad = this.OnLoad.bind(this)
    }
    componentDidMount() {
        this.OnLoad();
    }


    OnLoad = (e) => {
        var self = this;
        axios({
            method: 'get',
            url: 'http://localhost:4388/api/v1/user/GetUserByToken',
            params: {
                jtoken: localStorage.getItem("token"),
            }
        }).then(function (data) {
            console.log(data);
            console.log(data.data);
            self.setState({ userData: data.data });
        });
    }


    render() {
        var self = this;
        console.log(self.userData);
        if (!localStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        return (

            <body>
                <div>
                    <div class="col-sm-4 mt-4">
                        <div class="card" style={{ backgroundColor: "white", minHeight: "520px", maxHeight: "520px", borderColor: "#FF1801" }}>
                            <div class="card-body">
                                {/*                                    <img class="card-img-top" style={{ maxHeight: "200px" }} src={Race.imageUrl} alt="Card image cap" width="auto" height="auto" ></img>*/}
                                <h4 style={{ textAlign: "center", fontWeight: "bold" }} class="card-title">FullName: {data.fullname}</h4>
                                <hr class="solid"></hr>
                                <p style={{ textAlign: "center" }} class="card-text">Email: {data.email}</p>
                                <p style={{ textAlign: "center" }} class="card-text">Admin: {data.isAdmin}</p>
                                <p style={{ textAlign: "center" }} class="card-text">UserId: {data.UserId}</p>
                            </div>
                            <div class="card-footer" style={{ backgroundColor: "darkgray", Height: "auto", maxHeight: "auto" }} >
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        )
    }
}