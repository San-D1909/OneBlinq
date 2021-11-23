import axios from 'axios';
import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardImg from 'reactstrap/lib/CardImg';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { NavMenu } from '../components/NavMenu';
import "./CSS/Register.css";

export class Register extends Component {
    static displayName = Register.name

    constructor(props) {
        super(props);
        this.state = {
            mail: '',
            fullname: '',
            password: '',
            passwordConfirmation: '',
            companyName: '',
            zipCode: '',
            street: '',
            country: '',
            houseNumber: '',
            bTWNumber: '',
            kVKNumber: '',
            phoneNumber: '',
            hasCompany: false
        }
        this.handleRegister = this.handleRegister.bind(this)
    }

    handleRegister = event => {
        event.preventDefault();
        var company = {
            companyName: this.state.companyName,
            zipCode: this.state.zipCode,
            street: this.state.street,
            country: this.state.country,
            houseNumber: this.state.houseNumber,
            bTWNumber: this.state.bTWNumber,
            kVKNumber: this.state.kVKNumber,
            phoneNumber: this.state.phoneNumber
        };
        var user = {
            mail: this.state.mail,
            fullname: this.state.fullname,
            password: this.state.password,
            passwordConfirmation: this.state.passwordConfirmation
        };

        var userdata = {};
        userdata.user = user;
        userdata.company = company;
        axios({
            method: 'post',
            url: 'http://localhost:4388/api/v1/Auth/Register',
            dataType: "json",
            data: userdata
        }).then(data => console.log(data))
        
    }

    render() {
        return (
            <>
                <NavMenu />
                <div className="row p-0 mx-auto registercontainer d-lg-none d-flex">
                    <div className="col-12 col-lg-6 p-1">
                        <Card className="h-100">
                            <CardBody>
                                <h1 className="text-center">Register</h1>
                                <div className="col-12">
                                    <Form>
                                        <div className="py-2">
                                            <Label for="fullname">Full name</Label>
                                            <Input type="text" onChange={(e) => this.setState({ fullname: e.target.value })} name="fullname" />
                                        </div>
                                        <div className="py-2">
                                            <Label for="email">Email</Label>
                                            <Input type="text" onChange={(e) => this.setState({ mail: e.target.value })} name="email" />
                                        </div>
                                        <div className="py-2">
                                            <Label for="password">Password</Label>
                                            <Input type="password" onChange={(e) => this.setState({ password: e.target.value })} name="password" />
                                        </div>
                                        <div className="py-2">
                                            <Label for="password">Confirm password</Label>
                                            <Input type="password" onChange={(e) => this.setState({ passwordConfirmation: e.target.value })} name="passwordConfirmation" />
                                        </div>
                                        <hr class="solid"></hr>
                                        <label for="myCheck">I have a company:</label>
                                        <input type="checkbox" id="myCheck" onChange={(e) => this.setState({ hasCompany: e.target.checked })} />
                                        {this.state.hasCompany &&
                                            <>
                                                <div className="py-2">
                                                    <Label for="companyName">Company Name</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ companyName: e.target.value })} name="companyName" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="zipCode">Zip Code</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ zipCode: e.target.value })} name="zipCode" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="street">Street Name</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ street: e.target.value })} name="street" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="houseNumber">House Number</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ houseNumber: e.target.value })} name="houseNumber" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="country">Country</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ country: e.target.value })} name="country" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="bTWNumber">BTW Number</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ bTWNumber: e.target.value })} name="bTWNumber" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="kVKNumber">KVK Number</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ kVKNumber: e.target.value })} name="kVKNumber" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="phoneNumber">PhoneNumber</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ phoneNumber: e.target.value })} name="phoneNumber" />
                                                </div>
                                            </>
                                        }
                                        <div className="py-2">
                                            <Button className="my-2 mr-2 ml-0 registerbutton" onClick={(e) => this.handleRegister(e)}>Register</Button>
                                            <Link className="m-2 loginlink" to="/login">Already have an account? Login here!</Link>
                                        </div>
                                    </Form>
                                </div>
                            </CardBody>
                        </Card>
                    </div>
                    <div className="col-12 col-lg-6 p-1">
                        <Card className="registerformcard h-100 order-last">
                            <CardBody className="p-0">
                                <CardImg className="h-100" src="./images/logo_big_wink_no_bg.svg" />
                            </CardBody>
                        </Card>
                    </div>
                </div>
                <div className="row p-0 mx-auto registercontainer d-none d-lg-flex">
                    <div className="col-12 col-lg-6 p-1">
                        <Card className="registerformcard h-100 order-last">
                            <CardBody className="p-0">
                                <CardImg className="h-100" src="./images/logo_big_wink_no_bg.svg" />
                            </CardBody>
                        </Card>
                    </div>
                    <div className="col-12 col-lg-6 p-1">
                        <Card className="h-100">
                            <CardBody>
                                <h1 className="text-center">Register</h1>
                                <div className="col-12">
                                    <Form>
                                        <div className="py-2">
                                            <Label for="fullname">Full name</Label>
                                            <Input type="text" onChange={(e) => this.setState({ fullname: e.target.value })} name="fullname" />
                                        </div>
                                        <div className="py-2">
                                            <Label for="email">Email</Label>
                                            <Input type="text" onChange={(e) => this.setState({ mail: e.target.value })} name="email" />
                                        </div>
                                        <div className="py-2">
                                            <Label for="password">Password</Label>
                                            <Input type="password" onChange={(e) => this.setState({ password: e.target.value })} name="password" />
                                        </div>
                                        <div className="py-2">

                                            <Label for="password">Confirm password</Label>
                                            <Input type="password" onChange={(e) => this.setState({ passwordConfirmation: e.target.value })} name="passwordConfirmation" />
                                        </div>
                                        <hr class="solid"></hr>
                                        <label for="myCheck">I have a company:</label>
                                        <input type="checkbox" id="myCheck" onChange={(e) => this.setState({ hasCompany: e.target.checked })} />
                                        {this.state.hasCompany &&
                                            <>
                                                <div className="py-2">
                                                    <Label for="companyName">Company Name</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ companyName: e.target.value })} name="companyName" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="zipCode">Zip Code</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ zipCode: e.target.value })} name="zipCode" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="street">Street Name</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ street: e.target.value })} name="street" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="houseNumber">House Number</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ houseNumber: e.target.value })} name="houseNumber" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="country">Country</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ country: e.target.value })} name="country" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="bTWNumber">BTW Number</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ bTWNumber: e.target.value })} name="bTWNumber" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="kVKNumber">KVK Number</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ kVKNumber: e.target.value })} name="kVKNumber" />
                                                </div>
                                                <div className="py-2">
                                                    <Label for="phoneNumber">PhoneNumber</Label>
                                                    <Input type="text" onChange={(e) => this.setState({ phoneNumber: e.target.value })} name="phoneNumber" />
                                                </div>
                                            </>
                                        }
                                        <div className="py-2">
                                            <Button className="my-2 mr-2 ml-0 registerbutton" onClick={(e) => this.handleRegister(e)}>Register</Button>
                                            <Link className="m-2 loginlink" to="/login">Already have an account? Login here!</Link>
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

