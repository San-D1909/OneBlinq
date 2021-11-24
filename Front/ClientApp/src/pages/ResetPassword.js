import axios from 'axios';
import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { NavMenu } from '../components/NavMenu';
import "./CSS/Login.css";


export function ResetPassword() {
    const search = useLocation().search;
    const email = new URLSearchParams(search).get('email');
    const token = new URLSearchParams(search).get('token');

    const [errorMessage, setErrorMessage] = useState('');
    const [password, setPassword] = useState('');
    const [passwordConfirm, setPasswordConfirm] = useState('');

    function submitPasswordReset(e) {
        e.preventDefault()
        if (!password) {
            setErrorMessage('No Password filled in')
            return
        } else if (!passwordConfirm) {
            setErrorMessage('No Confirm Password filled in')
            return
        } else if (password !== passwordConfirm) {
            setErrorMessage('Passwords do not match')
            return
        }

        axios({
            method: 'post',
            url: 'http://localhost:5000/api/v1/Auth/ResetPassword',
            headers: {
                'Content-Type': 'application/json',
            },
            data: {
                email,
                token,
                password,
                passwordConfirm
            }
        }).then(() => {

        })
    }

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
                                    {errorMessage ?? (
                                        <div className="py-2 col-12">
                                            <Label className="alert alert-danger col-12" role="alert">{errorMessage}</Label>
                                        </div>
                                    )}

                                    <div className="py-2">
                                        <Label for="email">Password</Label>
                                        <Input type="text" onChange={(e) => setPassword(e.target.value)} name="password" type="password" />
                                    </div>
                                    <div className="py-2">
                                        <Label for="email">Confirm Password</Label>
                                        <Input type="text" onChange={(e) => setPasswordConfirm(e.target.value)} name="passwordConfirm" type="password" />
                                    </div>

                                    <div className="py-2">
                                        <Button className="my-2 mr-2 ml-0 loginbutton" type="submit" onClick={submitPasswordReset}>Reset Password</Button>
                                    </div>
                                </Form>
                            </div>
                        </CardBody>
                    </Card>
                </div>
            </div>
        </>
    )
}
