import axios from 'axios';
import React, { Component } from 'react';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Link, Redirect } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardImg from 'reactstrap/lib/CardImg';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { NavMenu } from '../components/NavMenu';
import "./CSS/Login.css";


export class Login extends Component {
    static displayName = Login.name;
    constructor(props) {
        super(props)

        console.log(props)
        this.state = {
            email: '',
            password: '',
            token: '',
            hasError: false,
            errorMessage: '',
            loggedIn: false,
            token: null
        }
        this.handleLogin = this.handleLogin.bind(this)
    }

    componentDidUpdate() {
    }

    setSession = (token) => {
        localStorage.setItem("loggedin", true);
        localStorage.setItem("token", token.data);

        this.setState({ token: token.data, loggedIn: true });
    }

    handleLogin = (event) => {
        event.preventDefault();

        const email = this.state.email
        const password = this.state.password

        this.setState({ hasError: false, errorMessage: '' })

        if (email == '' || email == null) {
            this.setState({ hasError: true, errorMessage: "Email must be filled in!" })
            return;
        } else if (password == '' || password == null) {
            this.setState({ hasError: true, errorMessage: "Password must be filled in!" })
            return;
        }

        var self = this;
        axios({
            method: 'post',
            url: 'http://localhost:5000/api/v1/Auth/LogIn',
            data: { email, password }
        }).then(token => this.setSession(token)).catch(function (error) {
            if (error.message == "Request failed with status code 401") {
                self.setState({ hasError: true, errorMessage: "Username or Password is incorrect." })
            }
            return Promise.reject(error)
        });
    }


    render() {
        if (localStorage.getItem("loggedin")) {
            return (
                <Redirect to="/user/dashboard/" />
            )

        }

        return (
            <>
                <NavMenu />
                <div className="row p-0 mx-auto logincontainer">
                    <div className="col-12 col-lg-6 p-1">
                        <Card className="h-100">
                            <CardBody>
                                <h1 className="text-center">Login</h1>
                                <div className="col-12">
                                    <Form>
                                        {this.state.hasError ?? (
                                            <div className="py-2 col-12">
                                                <Label className="alert alert-danger col-12" role="alert">{this.state.errorMessage}</Label>
                                            </div>
                                        )
                                        }
                                        <div className="py-2">
                                            <Label for="email">Email</Label>
                                            <Input type="text" onChange={(e) => this.setState({ email: e.target.value })} name="email" />
                                        </div>
                                        <div className="py-2">
                                            <Label for="password">Password</Label>
                                            <Input type="password" onChange={(e) => this.setState({ password: e.target.value })} name="password" />
                                        </div>
                                        <div className="py-2">
                                            <Button className="my-2 mr-2 ml-0 loginbutton" onClick={(e) => this.handleLogin(e)}>Login</Button>
                                            <Link className="m-2 registerlink" to="/register">No account yet? Register here!</Link>
                                        </div>
                                    </Form>

                                    <div className="forgot-password">
                                        <Link className="m-2 passwordlink" to="/forgotpassword">Forgot your password?</Link>
                                    </div>

                                </div>
                            </CardBody>
                        </Card>
                    </div>
                    <div className="col-12 col-lg-6 p-1">

                        <Card className="loginformcard h-100 order-last">
                            <CardBody className="p-0">
                                <CardImg className="h-100" src="./images/logo_big_wink_no_bg.svg" />
                            </CardBody>
                        </Card>
                    </div>
                </div>
            </>
        );
    }
}
