import { NavMenu } from '../components/NavMenu';
import React, { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import "./CSS/VerifyEmail.css";

export default function VerifyEmail() {
    const search = useLocation().search;
    const email = new URLSearchParams(search).get('email');
    const { token } = useParams();

    const [state, setState] = useState();

    useEffect(() => {
        const payload = JSON.stringify({ email, token })
        fetch(process.env.REACT_APP_API_BACKEND + '/api/v1/Auth/ConfirmEmail', {
            method: 'POST',
            body: payload,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(d => setState(d.status))
    });


    return (
        <>
            <NavMenu />
            {state === 200
                ? <h1>You have verified your email</h1>
                : <h1>Verifying your email</h1>}
        </>
    )
}