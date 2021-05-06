from dependency_injector import containers, providers

from services.events import EventsService


class DiContainer(containers.DeclarativeContainer):
    events_service = providers.Singleton(EventsService)
