import { Link } from "react-router-dom"
import { Button, Header, Icon, Segment } from "semantic-ui-react"

export default function GettingReady(){
    return (
        <Segment>
            <Header>
                <Icon name="wait" />
                Sayfa hazırlanma aşamasında...
            </Header>
            <Segment.Inline>
                <Button as={Link} to="/home" primary>
                    Ana Sayfaya Dön
                </Button>
            </Segment.Inline>
        </Segment>
    )
}