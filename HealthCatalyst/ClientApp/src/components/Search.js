import React, { Component } from 'react';
import { Button, FormControl, FormGroup, ControlLabel, Form } from 'react-bootstrap';

export class Search extends Component {
    displayName = Search.name

    constructor(props) {
        super(props);
        this.state = { people: [], loading: false, query: '' };
    }

    searchByName = e => {

        console.log('in render search name')

        // Prevent button click from submitting form
        e.preventDefault();
        
        this.setState({ loading: true });

        const searchInput = this.state.query;
        const query = 'api/People/' + searchInput;

        fetch(query)
            .then(response => response.json())
            .then(data => {
                console.log(data)
                this.setState({ people: data, loading: false });
            });
    }

    renderPeopleTable() {
        if (this.state.people.length !== 0) {
            return (
                <table className='table'>
                    <thead>
                        <tr>
                            <th></th>
                            <th>First name</th>
                            <th>Last name</th>
                            <th>Age</th>
                            <th>Address</th>
                            <th>Interests</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.people.map(person =>
                            <tr key={person.firstName}>
                                <td><img id="ItemPreview" alt="Profile" src={'.' + person.photo} /></td>
                                <td>{person.firstName}</td>
                                <td>{person.lastName}</td>
                                <td>{person.age}</td>
                                <td>{person.address}</td>
                                <td>{person.interests}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            )
        }
        else {
            return ( <span></span> )
        }
    }

    getValidationState() {
        if (typeof this.state.query === 'string')
            return 'success';
        return 'error';
    }

    handleChange = e => {
        this.setState({ query: e.target.value });
    }

    submitHandler = e => {
        e.preventDefault();
    }

    renderSearchArea() {
        return (
            <div className='container'>
                <section className="section">
                    <Form inline className="form" id="searchForm" onSubmit={this.submitHandler}>
                        <FormGroup
                            controlId="formSearch"
                            validationState={this.getValidationState}
                        />
                        <FormControl
                            type="text"
                            value={this.state.query}
                            placeholder="Search for person"
                            className="input"
                            id="searchInput"
                            onChange={this.handleChange}
                        />
                        <Button bsStyle="primary" className="button is-info" onClick={this.searchByName}>
                            Search
                        </Button>
                    </Form>
                </section>
            </div>
        );
    }

    render() {
        return (
            <div>
                {this.renderSearchArea()}
                {this.renderPeopleTable()}
            </div>
        );
    }
}
