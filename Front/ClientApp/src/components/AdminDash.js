import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
//import { List, Datagrid, Textfield, Filter, SearchInput } from 'react-admin';
//import { Admin, resource } from 'react-admin';
//import lb4provider from 'react-admin-lb4';

//const AdminFilter = (props) => (<Filter{...props}>
//    <SearchInput placeholder='Username' source 'username'
//    resettable alwaysOn />
//       </ Filter>)


//function AdminDash(props) {
//    return (
//        <List {...props/>}filters={<CustomerFilter/>}>
//            <Datagrid>
//                <Textfield
//                    // Hierin comment je de rows van onze user/plugin tabel info source=''
//                />
//                {/* <Textfield source=''/>*/}
//                {/* <Textfield source=''/>*/}
//                {/* <Textfield source=''/>*/}

//            </Datagrid>

//        </List>
//        )
//}


export class AdminDash extends Component {
    static displayName = AdminDash.name;

    //fetchUserInfo() {
    //    // TODO: fetch plugins
    //    console.log('fetch')
    //    return [{
    //        id: "1",
    //        username: "hassan"
    //    }, {
    //        id: "1",
    //        username: "hassan"
    //    }, {
    //        id: "1",
    //        username: "hassan"
    //    }, {
    //        id: "1",
    //        username: "hassan"
    //    }, {
    //        id: "1",
    //        username: "hassan"
    //    }, {
    //        id: "1",
    //        username: "hassan"
    //    }]
    //}

    render() {
        return (
            <>
                <NavMenu />
            </>
            );
    }
}
            //Misschien nodig in de future (in de return())
            //<Admin dataProvider={lb4Provider('http://localhost:4388')}<Admin />
            //<Resource name = 'users' list = {UserList} />
            //'https://youtu.be/4y2FFEmPW7I'