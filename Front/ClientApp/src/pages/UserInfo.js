import * as React from "react";
import { Component } from "react";
import { email } from "react-admin";
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import axios from 'axios';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Link, Redirect } from 'react-router-dom';

export class UserInfo extends Component {
    static displayName = UserInfo.name;

    constructor(props) {
        super(props);

        this.state = {
            fullname: '',
            mail: '',
            password: '',
            companyname: '',
            userData: '',
            companyData: '',
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
            url: 'http://localhost:5000/api/v1/user/GetUserByToken',
            params: {
                jtoken: localStorage.getItem("token"),
            }
        }).then((data) => {
            console.log(data);
            self.setState({ userData: data.data.userById, companyData: data.data.userCompany });
            console.log(this.userData);
        });
    }


    render() {
        if (!localStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        console.log(this.userData);
        return (
            <body>
                <div>
                    <div class="col-sm-4 mt-4">
                        <div class="card" style={{ backgroundColor: "white", minHeight: "520px", maxHeight: "520px", borderColor: "#FF1801" }}>
                            <div class="card-body">
                                <h4 style={{ textAlign: "center", fontWeight: "bold" }} class="card-title">FullName: {/*{this.state.userData.userbyid.fullName}*/}</h4>
                                <hr class="solid"></hr>
{/*                                <p style={{ textAlign: "center" }} class="card-text">CompanyID: {this.state.userData.company}</p>
                                <p style={{ textAlign: "center" }} class="card-text">UserId: {this.state.userData.userId}</p>*/}
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