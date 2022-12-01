import axios from 'axios'
import { XMLParser } from 'fast-xml-parser'

export default class Soap {
    host = 'http://localhost:52056/LibraryWebService.asmx'

    queryHeaders = {
        'Content-Type': 'text/xml; charset=unicode',
        'Access-Control-Allow-Origin' : '*',
        'Access-Control-Allow-Methods':'GET, POST, OPTIONS'
    }

    xmlQueryByAuthor = (author) =>
    `<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <GetBooksByAuthor xmlns="http://tempuri.org/">
          <author>${author}</author>
        </GetBooksByAuthor>
      </soap:Body>
    </soap:Envelope>`

    xmlQueryByCategory = (category) =>
    `<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <GetBooksByCategory xmlns="http://tempuri.org/">
          <category>${category}</category>
        </GetBooksByCategory>
      </soap:Body>
    </soap:Envelope>`

    xmlQueryByTitle = (title) =>
    `<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <GetBooksByTitle xmlns="http://tempuri.org/">
          <title>${title}</title>
        </GetBooksByTitle>
      </soap:Body>
    </soap:Envelope>`

    constructor() { }

    async GetByAuthor(author) {
      return axios.post(this.host, this.xmlQueryByAuthor(author), { headers: this.queryHeaders })
          .then( response => {
            const parser = new XMLParser();
            let json = parser.parse(response?.request?.response);
            return json['soap:Envelope']['soap:Body']['GetBooksByAuthorResponse']['GetBooksByAuthorResult']['Book']
          }).catch(error => {
              console.log(error)
          });
    }

    async GetByCategory(category) {
      return axios.post(this.host, this.xmlQueryByCategory(category), { headers: this.queryHeaders })
          .then( response => {
            const parser = new XMLParser();
            let json = parser.parse(response?.request?.response);
            return json['soap:Envelope']['soap:Body']['GetBooksByCategoryResponse']['GetBooksByCategoryResult']['Book']
          }).catch(error => {
              console.log(error)
          });
    }

    async GetByTitle(title) {
      return axios.post(this.host, this.xmlQueryByTitle(title), { headers: this.queryHeaders })
          .then( response => {
            const parser = new XMLParser();
            let json = parser.parse(response?.request?.response);
            return json['soap:Envelope']['soap:Body']['GetBooksByTitleResponse']['GetBooksByTitleResult']['Book']
          }).catch(error => {
              console.log(error)
          });
    }
}
