import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import { PluginCard } from './PluginCard'
import { Footer } from './Footer'

function PluginList(props) {
    const { plugins } = props
    const listItems = plugins.map((plugin, i) =>
        <div key={i} className="col-12 col-lg-4 col-md-6 py-3">
            <PluginCard data={plugin} />
        </div>
    )
    return (
        <div className="row">
            {listItems}
        </div>
    )
}

function BundleCard(props) {
    const bundleCardTitleStyle = {
        backgroundColor: "#deeff4",
        color: "#02021E"
    }
    const textStyle = {
        color: "inherit"
    }
    const textDecoNone = {
        textDecoration: "none"
    }
    const { bundle } = props
    return (
        <a style={textDecoNone} className="card plugin" href={bundle.id}>
            <div className="card-body">
                <img src="https://via.placeholder.com/344x216.png" alt={bundle.title} />
            </div>
            <div style={bundleCardTitleStyle} className="card-footer">
                <span style={textStyle}>{bundle.title}</span>
                <span style={textStyle} className="float-end">
                    <span style={textStyle}><i className="far fa-sm fa-comment"></i> 123</span>
                    <span style={textStyle} className="pl-2"><i className="fas fa-sm fa-arrow-down"></i> 1.7k</span>
                </span>
            </div>
        </a >
     )
}

function PluginBundleList(props) {
    const { bundles } = props
    const listItems = bundles.map((bundle, i) =>
        <div key={i} className="col-12 col-lg-4 col-md-6 py-3">
            <BundleCard bundle={bundle} />
        </div>
    )

    return (
        <div className="row">
            {listItems}
        </div>
    )
}

export class Plugins extends Component {
    static displayName = Plugins.name;
    constructor(props) {
        super(props)
        this.state = {
            plugins: [],
            bundles: [],
            hover: false
        }
        this.addPlugin = this.addPlugin.bind(this)
        this.fetchPlugins = this.fetchPlugins.bind(this)
        this.fetchBundles = this.fetchBundles.bind(this)
    }

    componentDidMount() {
        this.setState({ plugins: this.fetchPlugins(), bundles: this.fetchBundles(), hover: false})
    }

    fetchBundles() {
        // TODO: fetch bundles
        console.log("fetch bundles")
        return [{
            id: 1,
            title: "figma-bundle",
            plugins: [{
                id: 1,
                title: "forms",
                price: 5.00
            }]
        }]
    }

    fetchPlugins() {
        // TODO: fetch plugins
        console.log('fetch')
        return [{
            id: "1",
            title: "forms"
        }, {
            id: "2",
            title: "squares"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }]
    }

    addPlugin() {
        // TODO: remove, meant for testing during development
        const p = {
            id: "1",
            title: "forms"
        }
        this.setState({ plugins: this.state.plugins.concat(p).concat(p).concat(p)})
    }

    hrStyling = {
        height: "2px",
        opacity: 0.80,
        color: "#edeffc"
    }


    showMoreHoverStyling = {

    }

    toggleHover = () => {
        this.setState({ hover: !this.state.hover })
    }


    render() {
        var btnStyle;
        if (this.state.hover) {
            btnStyle = {
                color: "#edeffc",
                borderColor: "#edeffc",
                backgroundColor: "#efa9ae"
            }
        } else {
            btnStyle = {
                color: "#edeffc",
                borderColor: "#edeffc",
                backgroundColor: "transparent"
            }
        }
        return (
            <>
                <NavMenu />
                <h1 className="row m-0 justify-content-center" style={{ color: '#edeffc' }} >Plugin Bundles</h1>
                <hr style={this.hrStyling} className="container" />
                <div className="container">
                    <PluginBundleList bundles={this.state.bundles} />
                </div>
                <h1 className="row m-0 justify-content-center" style={{ color: '#edeffc' }} >Plugins</h1>
                <hr style={this.hrStyling} className="container"  />
                <div className="container">
                    <PluginList plugins={this.state.plugins} />
                    <div className="row m-0 justify-content-center">
                        <button style={btnStyle} onClick={this.addPlugin} onMouseEnter={this.toggleHover} onMouseLeave={this.toggleHover} className="btn btn-outline-pastel w-25 m-2"><span>Show More</span></button>
                    </div>
                </div>
                <Footer />
            </>
        );
    }
}
