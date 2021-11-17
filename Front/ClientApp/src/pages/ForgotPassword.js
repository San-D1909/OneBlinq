﻿import React, { Component, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { NavMenu } from '../components/NavMenu';
import axios from 'axios'
import "./CSS/Login.css";


export class ForgotPassword extends Component {

    static displayName = ForgotPassword.name;
    constructor(props) {
        super(props)

        console.log(props)
        this.state = {
            email: '',
            hasError: false,
            errorMessage: '',
        }
        this.handleSubmit = this.handleSubmit.bind(this)
    }


    componentDidUpdate() {
    }

    handleSubmit = (event) => {
        event.preventDefault();

        const email = this.state.email

        this.setState({ hasError: false, errorMessage: '' })

        if (email == '' || email == null) {
            this.setState({ hasError: true, errorMessage: "Email must be filled in!" })
            return;
        }

        axios({
            method: 'post',
            url: 'http://localhost:4388/api/v1/Auth/ForgotPassword',
            data: { email }
        }).then(response => {
            window.location.href = "/login"
        });
    }

    render() {
        return (
            <>

                <NavMenu />
                <div className="row p-0 mx-auto logincontainer">
                    <div className="col-12 col-lg-6 p-1">
                        <Card className="h-100">
                            <CardBody>
                                <h1 className="text-center">Reset Password</h1>
                                <div className="col-12">
                                    <Form>
                                        {this.state.hasError ? (
                                            <div className="py-2 col-12">
                                                <Label className="alert alert-danger col-12" role="alert">{this.state.errorMessage}</Label>
                                            </div>
                                        ) : (<></>)
                                        }

                                        <div className="py-2">
                                            <Label for="email">Email</Label>
                                            <Input type="text" onChange={(e) => this.setState({ email: e.target.value })} name="email" />
                                        </div>

                                        <div className="py-2">
                                            <Button className="my-2 mr-2 ml-0 loginbutton" onClick={(e) => this.handleSubmit(e)}>Submit</Button>
                                        </div>

                                    </Form>
                                </div>
                            </CardBody>
                        </Card>
                    </div>
                </div>
            </>
        );
    }
}
