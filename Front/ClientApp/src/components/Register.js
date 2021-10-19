import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import ButtonGroup from 'reactstrap/lib/ButtonGroup';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardFooter from 'reactstrap/lib/CardFooter';
import CardImg from 'reactstrap/lib/CardImg';
import CardTitle from 'reactstrap/lib/CardTitle';
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
            <div className="row w-100 p-0 m-0">
                <div className="col-12 col-md-6 p-1">
                    <Card className="registerformcard h-100">
                        <CardBody className="p-0">
                            <CardImg className="h-100" src="./images/logo_big_wink_no_bg.svg" />
                        </CardBody>
                    </Card>
                </div>
                <div className="col-12 col-md-6 p-1">
                    <Card className="h-100">
                        <CardBody>
                            <CardImg className="registerlogo" src="./images/logo_black_name_only.svg" />
                            <div className="col-12">
                                <Form>
                                    <div className="py-2">
                                        <Label for="username">Username</Label>
                                        <Input type="text" name="username"/>
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

            </div>
        </>
    );
  }
}
