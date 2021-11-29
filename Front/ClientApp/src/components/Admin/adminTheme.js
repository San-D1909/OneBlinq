import { defaultTheme } from "react-admin";
import createMuiTheme from "@material-ui/core/styles/createMuiTheme";
import merge from "lodash/merge";
import React from 'react'
import { AppBar } from 'react-admin'
import Typography from '@material-ui/core/Typography'
import { withStyles } from '@material-ui/core/styles'
import IconButton from '@material-ui/core/IconButton'
import BackIcon from '@material-ui/icons/ArrowBack'

const styles = {
    title: {
        flex: 1,
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
        overflow: 'hidden',
    },
    spacer: {
        flex: 1,
    },
}

const MyAppBar = withStyles(styles)(({ classes, ...props }) => (
    <AppBar {...props}>
        <Typography
            variant="title"
            color="inherit"
            className={classes.title}
            id="react-admin-title"
        />
        <span className={classes.spacer} />
        <IconButton color="inherit" >
            <BackIcon />
        </IconButton>
    </AppBar>
))

export default MyAppBar

export const adminTheme = createMuiTheme(
    merge({}, defaultTheme, {
        palette: {
            // Your theme goes here
            // Write the following code to have an orange app bar. We'll explain it later in this article.
            secondary: {
                main: "##efa9ae", // Not far from orange
            },
        }
    })
);