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
import "./Login.css";

export class Login extends Component {
  static displayName = Login.name;

  render () {
    return (
        <>
            <NavMenu />
            <div className="row w-100 p-0 m-0">
                <div className="col-12 col-md-6 p-1">
                    <Card className="h-100">
                        <CardBody>
                            <CardImg className="loginlogo" src="./images/logo_black_name_only.svg" />
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
                                        <Button className="my-2 mr-2 ml-0 loginbutton">Login</Button>
                                        <Link className="m-2 registerlink">No account yet? Register here!</Link>
                                    </div>
                                </Form>
                            </div>
                        </CardBody>
                    </Card>
                </div>
                <div className="col-12 col-md-6 p-1">
                    <Card className="loginformcard">
                        <CardBody className="p-0">
                            <CardImg src="./images/logo_big_wink.svg" />
                        </CardBody>
                    </Card>
                </div>
            </div>
        </>
    //   <div>
    //     <h1>Hello, world!</h1>
    //     <p>Welcome to your new single-page application, built with:</p>
    //     <ul>
    //       <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
    //       <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
    //       <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
    //     </ul>
    //     <p>To help you get started, we have also set up:</p>
    //     <ul>
    //       <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
    //       <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
    //       <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
    //     </ul>
    //     <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
    //   </div>
    );
  }
}
