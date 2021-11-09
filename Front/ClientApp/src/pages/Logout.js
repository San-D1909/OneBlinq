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
import ReactSession from 'react-client-session/dist/ReactSession';
import { Redirect } from 'react-router-dom';


export class Logout extends Component {
    static displayName = Logout.name;

    render() {

        if (ReactSession.get("loggedin")) {
            ReactSession.remove("loggedin");
            ReactSession.remove("token");

            return (
                <Redirect to="/login" />
            )
        }
     }
}
