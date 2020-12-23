import React, { Component, createRef } from "react";
import "./URLShortener.css";

export class URLShortener extends Component {
    static displayName = URLShortener.name;

    constructor(props) {
        super(props);
        this.shortenUrl = this.shortenUrl.bind(this);
        this.urlInputField = React.createRef();
        this.responseArea = React.createRef();
    }

    shortenUrl() {
        const url = this.urlInputField.current.value;
        console.log(url);

        if (URLShortener.isValidUrl(url)) {
            
        } else {
            this.setResponseArea("Invalid url");
        }
    }

    setResponseArea(response) {
        this.responseArea.current.innerText = errorMessage;
        this.responseArea.current.hidden = false;
    }

    static isValidUrl(str) {
        var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
            '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
            '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
            '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
            '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
            '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
        return !!pattern.test(str);
    }

    render() {
        return (
            <div className="centered">
                <h2 className="landing-header">Shorten your URLs</h2>
                <input className="url-input"
                    ref={this.urlInputField}
                    placeholder="Input a URL to shorten here" />
                <button className="submit" onClick={this.shortenUrl}>Shorten</button>

                <div className="response-area" hidden ref={ this.responseArea }>
                
                </div>

            </div>
        );
    }
}