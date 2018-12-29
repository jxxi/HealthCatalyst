import React, { Component } from 'react';

export class Search extends Component {
    displayName = Search.name

    constructor(props) {
        super(props);
        this.state = { people: [], loading: false };

        fetch('api/People')
            .then(response => response.json())
            .then(data => {
                console.log(data)
                this.setState({ people: data, loading: false });
            });
    }

    static renderSearchArea(people) {
        return (
            <div className='container'>
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
                        {people.map(person =>
                            <tr key={person.firstName}>
                                <td><img id="ItemPreview" alt="No Image" src={'.' + person.photo}/></td>
                                <td>{person.firstName}</td>
                                <td>{person.lastName}</td>
                                <td>{person.age}</td>
                                <td>{person.address}</td>
                                <td>{person.interests}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = Search.renderSearchArea(this.state.people);

        return (
            <div>
                {contents}
            </div>
        );
    }
}
