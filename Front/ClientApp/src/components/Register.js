import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardImg from 'reactstrap/lib/CardImg';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { NavMenu } from './NavMenu';
import "./Register.css";

export class Register extends Component {
  static displayName = Register.name;

  render () {
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
                                        <Label for="email">Email</Label>
                                        <Input type="text" name="email"/>
                                    </div>
                                    <div className="py-2">
                                        <Label for="password">Password</Label>
                                        <Input type="password" name="password"/>
                                    </div>
                                    <div className="py-2">
                                        <Label for="password">Confirm password</Label>
                                        <Input type="password" name="password"/>
                                    </div>
                                    <div className="py-2">
                                        <Button className="my-2 mr-2 ml-0 registerbutton">Register</Button>
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
                                        <Label for="email">Email</Label>
                                        <Input type="text" name="email" />
                                    </div>
                                    <div className="py-2">
                                        <Label for="password">Password</Label>
                                        <Input type="password" name="password" />
                                    </div>
                                    <div className="py-2">
                                        <Label for="password">Confirm password</Label>
                                        <Input type="password" name="password" />
                                    </div>
                                    <div className="py-2">
                                        <Button className="my-2 mr-2 ml-0 registerbutton">Register</Button>
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
