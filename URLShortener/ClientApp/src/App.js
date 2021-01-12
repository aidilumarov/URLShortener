import React, { Component } from 'react';
import { Route } from 'react-router';

import { UrlShortener } from './components/UrlShortener';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (

        <Route exact path='/' component={UrlShortener} />
    );
  }
}
