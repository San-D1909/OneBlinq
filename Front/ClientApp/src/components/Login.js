import React, { Component, useEffect } from 'react';
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
import "./Login.css";
import axios from 'axios'


export class Login extends Component {
    static displayName = Login.name;
    constructor(props) {
        super(props)
        this.state = {
            mail: '',
            password: ''
        }
        this.handleLogin = this.handleLogin.bind(this)
    }

    handleLogin = event => {
        event.preventDefault();
        const mail = this.state.mail
        const password = this.state.password
        axios({
            method: 'post',
            url: 'http://localhost:4388/api/v1/Auth/LogIn',
            data: { mail,password }
        }).then(data => console.log(data));
    }

  render () {
    return (
        <>
            <NavMenu />
            <div className="row p-0 mx-auto logingcontainer">
                <div className="col-12 col-lg-6 p-1">
                    <Card className="h-100">
                        <CardBody>
                            <h1 className="text-center">Register</h1>
                            <div className="col-12">
                                <Form>
                                    <div className="py-2">
                                        <Label for="email">Email</Label>
                                        <Input type="text" onChange={(e) => this.setState({mail: e.target.value})} name="email"/>
                                    </div>
                                    <div className="py-2">
                                        <Label for="password">Password</Label>
                                        <Input type="password" onChange={(e) => this.setState({password: e.target.value})} name="password"/>
                                    </div>
                                    <div className="py-2">
                                        <Button className="my-2 mr-2 ml-0 loginbutton" onClick={(e) => this.handleLogin(e)}>Login</Button>
                                        <Link className="m-2 registerlink" to="/register">No account yet? Register here!</Link>
                                    </div>
                                </Form>
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
